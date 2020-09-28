using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class LevelGenerator : MonoBehaviour
{
	[SerializeField] private GameObject[] wallPeices;
	[SerializeField] private GameObject sprite;// debugging
	[SerializeField] private Color[] colors;

	private Dictionary<Vector2, wallPiece> allWalls = new Dictionary<Vector2, wallPiece>();
	private struct wallPiece
	{
		public WallType wallType;
		public GameObject myGameObject;

		public wallPiece(WallType _wallType, GameObject _myGameObject)
		{
			wallType = _wallType;
			myGameObject = _myGameObject;
		}
	}
	public enum WallType
	{
		Empty,
		OutsideCorner,
		OutsideWall,
		InsideCorner,
		InsideWall,
		Pellet,
		PowerPeller,
		JuncitonPiece,
	}

	private int[,] levelMap =
	{
		{1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 7},
		{2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4},
		{2, 5, 3, 4, 4, 3, 5, 3, 4, 4, 4, 3, 5, 4},
		{2, 6, 4, 0, 0, 4, 5, 4, 0, 0, 0, 4, 5, 4},
		{2, 5, 3, 4, 4, 3, 5, 3, 4, 4, 4, 3, 5, 3},
		{2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5},
		{2, 5, 3, 4, 4, 3, 5, 3, 3, 5, 3, 4, 4, 4},
		{2, 5, 3, 4, 4, 3, 5, 4, 4, 5, 3, 4, 4, 3},
		{2, 5, 5, 5, 5, 5, 5, 4, 4, 5, 5, 5, 5, 4},
		{1, 2, 2, 2, 2, 1, 5, 4, 3, 4, 4, 3, 0, 4},
		{0, 0, 0, 0, 0, 2, 5, 4, 3, 4, 4, 3, 0, 3},
		{0, 0, 0, 0, 0, 2, 5, 4, 4, 0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 2, 5, 4, 4, 0, 3, 4, 4, 0},
		{2, 2, 2, 2, 2, 1, 5, 3, 3, 0, 4, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 4, 0, 0, 0},
	};

	void Start()
	{
		SetValueTopleft();
		setValueTopleftMirror(levelMap);

		setValueBotttomLeft(levelMap);
		setValueBotttomRight(levelMap);
		SetRotationForEachWall();
	}

	private void SetRotationForEachWall()
	{
		foreach (var wall in allWalls)
		{
			bool wallOnLeft = false;
			bool wallOnRight = false;
			bool wallAbove = false;
			bool wallUnder = false;
			int wallNextToPosition = 0;

			if (allWalls.TryGetValue(wall.Key + new Vector2(-1, 0), out var wallLeft))
			{
				if (IsThereAWallThere(wallLeft.wallType))
				{
					wallOnLeft = true;
					wallNextToPosition++;
				}
			}

			//top wall
			if (allWalls.TryGetValue(wall.Key + new Vector2(0, 1), out var AboveWall))
			{
				if (IsThereAWallThere(AboveWall.wallType))
				{
					wallAbove = true;
					wallNextToPosition++;
				}
			}
			//right wall	
			if (allWalls.TryGetValue(wall.Key + new Vector2(1, 0), out var wallRight))
			{
				if (IsThereAWallThere(wallRight.wallType))
				{
					wallOnRight = true;
					wallNextToPosition++;
				}
			}

			//bottom wall
			if (allWalls.TryGetValue(wall.Key + new Vector2(0, -1), out var WallUnder))
			{
				if (IsThereAWallThere(WallUnder.wallType))
				{
					wallUnder = true;
					wallNextToPosition++;
				}
			}

			Vector3 wallRotation = Vector3.zero;

			if (wall.Value.wallType == WallType.InsideWall || wall.Value.wallType == WallType.OutsideWall)
			{
				if (wallUnder && wallAbove)
				{
					wallRotation.z = 90;
				}
			}

			if (wall.Value.wallType == WallType.InsideCorner || wall.Value.wallType == WallType.OutsideCorner)
			{
				if (wallAbove && wallOnRight)
				{
					wallRotation.z = 90;
				}

				if (wallAbove && wallOnLeft)
				{
					wallRotation.z = 180;
				}

				if (wallUnder && wallOnLeft)
				{
					wallRotation.z = 270;
				}

				if (wallNextToPosition >= 3 && (wall.Value.wallType == WallType.InsideCorner || wall.Value.wallType == WallType.OutsideCorner))
				{
					bool topRightCornerWall = false;
					bool bottomRightCornerWall = false;
					bool topleftCornerWall = false;
					bool bottomLeftCornerWall = false;

					if (allWalls.TryGetValue(wall.Key + new Vector2(1, 1), out var topRightCorner))
					{
						if (IsThereAWallThere(topRightCorner.wallType))
						{
							topRightCornerWall = true;
						}
						else
						{
							Instantiate(sprite, topRightCorner.myGameObject.transform.position, Quaternion.identity);// debugging
						}
					}

					if (allWalls.TryGetValue(wall.Key + new Vector2(1, -1), out var bottomRightCorner))
					{
						if (IsThereAWallThere(bottomRightCorner.wallType))
						{
							bottomRightCornerWall = true;
						}
						else
						{
							Instantiate(sprite, bottomRightCorner.myGameObject.transform.position, Quaternion.identity);// debugging
						}
					}

					if (allWalls.TryGetValue(wall.Key + new Vector2(-1, 1), out var topleftCorner))
					{
						if (IsThereAWallThere(topleftCorner.wallType))
						{
							topleftCornerWall = true;
						}
						else
						{
							Instantiate(sprite, topleftCorner.myGameObject.transform.position, Quaternion.identity);// debugging
						}
					}

					if (allWalls.TryGetValue(wall.Key + new Vector2(-1, -1), out var bottomLeftCorner))
					{
						if (IsThereAWallThere(bottomLeftCorner.wallType))
						{
							bottomLeftCornerWall = true;
						}
						else
						{
							Instantiate(sprite, bottomLeftCorner.myGameObject.transform.position, Quaternion.identity);// debugging
						}
					}

					int amountofbob = 0;
					if (wallNextToPosition == 4)
					{
						if (!topRightCornerWall)
						{
							wallRotation.z = 90;
							amountofbob++;
						}

						if (!bottomRightCornerWall)
						{
							wallRotation.z = 0;
							amountofbob++;
						}

						if (!topleftCornerWall)
						{
							wallRotation.z = -180;
							amountofbob++;
						}

						if (!bottomLeftCornerWall)
						{
							wallRotation.z = -90;
							amountofbob++;
						}

						if (amountofbob > 1)
						{
							Debug.LogError($"too many rotation");
						}
					}
				}
			}

			if (wall.Value.wallType != WallType.JuncitonPiece)
			{
				wall.Value.myGameObject.transform.rotation = Quaternion.Euler(wallRotation);
			}
		}
	}

	private void SetValueTopleft()
	{
		float xValue = 0;
		float yValue = 0;
		var amountOfRows = levelMap.GetUpperBound(0) + 1;
		for (int i = 0; i < amountOfRows; i++, yValue--, xValue = 0)
		{
			for (int k = 0; k < 14; k++, xValue++)
			{
				var currentWall = (WallType)levelMap[i, k];

				var block = Instantiate(wallPeices[(int)currentWall], new Vector3(xValue, yValue, 0), Quaternion.identity);

				block.transform.parent = transform;

				block.GetComponentInChildren<SpriteRenderer>().color = colors[(int)currentWall];

				allWalls.Add(new Vector2(xValue, yValue), new wallPiece(currentWall, block));

				if (currentWall == WallType.JuncitonPiece)
				{
					block.transform.rotation = Quaternion.Euler(0, 0, -90);
				}
			}
		}
	}


	public void setValueTopleftMirror(int[,] reversedLevelMap)
	{
		float xValue = 14;
		float yValue = -0;

		var amountOfRows = reversedLevelMap.Length / 14;
		for (int i = 0; i < amountOfRows; i++, yValue--, xValue = 14)
		{
			for (int k = 13; k > -1; k--, xValue++)
			{
				var currentWall = (WallType)reversedLevelMap[i, k];

				var block = Instantiate(wallPeices[(int)currentWall], new Vector3(xValue, yValue, 0), Quaternion.identity);
				block.transform.parent = transform;
				block.GetComponentInChildren<SpriteRenderer>().color = colors[(int)currentWall];
				allWalls.Add(new Vector2(xValue, yValue), new wallPiece(currentWall, block));


				if (currentWall == WallType.JuncitonPiece)
				{
					block.transform.rotation = Quaternion.Euler(0, 0, -90);
				}
			}
		}
	}

	public void setValueBotttomLeft(int[,] reversedLevelMap)
	{
		float xValue = 0;
		float yValue = -15;

		var amountOfRows = reversedLevelMap.GetUpperBound(0);

		for (int i = amountOfRows - 1; i > -1; i--, yValue--, xValue = 0)
		{
			for (int k = 0; k < 14; k++, xValue++)
			{
				var currentWall = (WallType)reversedLevelMap[i, k];

				var block = Instantiate(wallPeices[(int)currentWall], new Vector3(xValue, yValue, 0), Quaternion.identity);
				block.transform.parent = transform;
				block.GetComponentInChildren<SpriteRenderer>().color = colors[(int)currentWall];
				allWalls.Add(new Vector2(xValue, yValue), new wallPiece(currentWall, block));

				if (currentWall == WallType.JuncitonPiece)
				{
					block.transform.rotation = Quaternion.Euler(0, 0, 90);
				}
			}
		}
	}

	public void setValueBotttomRight(int[,] reversedLevelMap)
	{
		float xValue = 14;
		float yValue = -15;

		var amountOfRows = reversedLevelMap.GetUpperBound(0);


		for (int i = amountOfRows - 1; i > -1; i--, yValue--, xValue = 14)
		{
			for (int k = 13; k > -1; k--, xValue++)
			{
				var currentWall = (WallType)reversedLevelMap[i, k];

				var block = Instantiate(wallPeices[(int)currentWall], new Vector3(xValue, yValue, 0), Quaternion.identity);
				block.transform.parent = transform;

				block.GetComponentInChildren<SpriteRenderer>().color = colors[(int)currentWall];
				allWalls.Add(new Vector2(xValue, yValue), new wallPiece(currentWall, block));

				if (currentWall == WallType.JuncitonPiece)
				{
					block.transform.rotation = Quaternion.Euler(0, 0, 90);
				}
			}
		}
	}

	private bool IsThereAWallThere(WallType levelMap)
	{
		return levelMap != WallType.Empty && levelMap != WallType.Pellet && levelMap != WallType.PowerPeller;
	}
}