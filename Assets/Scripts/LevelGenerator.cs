using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject CenterPoint;
    [SerializeField] private GameObject[] wallPeices;
    //private Dictionary<>
    private int[,] levelMap =
           {
                {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
                {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
                {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
                {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
                {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
                {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
                {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
                {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
                {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
                {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
                {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
                {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
                {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
                {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
                {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
                };
    private int[,] levelMap2 =
           {
                {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
                {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
                {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
                {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
                {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
                {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
                {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
                {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
                {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
                {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
                {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
                {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
                {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
                {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
                };
    // Start is called before the first frame update
    void Start()
    {

        setValueTopleft();
       setValueTopRight();
        setValueBottomLeft();
       setValueBottomRight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setValueTopleft()
    {
        float xValue = 0;
        float zValue = 0;
        int count = 0;
       // print($"{levelMap[0, 5]}");


      //  var amountOfRows = levelMap.Length / 14;
       // for (int i = 0; i < amountOfRows; i++)
      //  {

          //  for (int k = 0; k < 14; k++)
          //  {
          //      bool wallOnLeft = false;
           //     bool wallOnRight = false;
           //     bool wallAbove = false;
           //     bool wallUnder = false;
           //     if (k == 0)
            //    {
            //        wallOnLeft = false;
           //     }
           //     if (i == 0)
           //     {
           //         wallAbove = false;
           //     }
                //if(levelMap[i,k])


                //}




         //   }
            //for (int i = 0; i < levelMap.Length; i++)
            //{
            //    //if(i-0)

            //}

            foreach (int interger in levelMap)
            {
                count += 1;
                //if (interger != 0)
                //{
                    Instantiate(wallPeices[interger], new Vector3(xValue, 0, zValue), Quaternion.Euler(90, 0, 0));
                //}
                xValue += 0.3f;
                if (count == 14)
                {
                    count = 0;
                    xValue = 0;
                    zValue -= 0.3f;
                }
            }

        
    }
    public void setValueTopRight()
    {
        float xValue = 8f;
        float zValue = 0;
        int count = 0;
        foreach (int interger in levelMap2)
        {
            count += 1;
            Instantiate(wallPeices[interger], new Vector3(xValue, 0, zValue), Quaternion.Euler(90, 0, 0));
            xValue -= 0.3f;
            if (count == 14)
            {
                count = 0;
                xValue = 8f;
                zValue -= 0.3f;
            }
        }

    }
    public void setValueBottomLeft()
    {
        float xValue = 0;
        float zValue = -8.6f;
        int count = 0;
        foreach (int interger in levelMap)
        {
            count += 1;
            Instantiate(wallPeices[interger], new Vector3(xValue, 0, zValue), Quaternion.Euler(90, 0, 0));        
            xValue += 0.3f;
            if (count == 14)
            {
                count = 0;
                xValue = 0;
                zValue += 0.3f;
            }
        }
    }
    public void setValueBottomRight()
    {
        float xValue = 8f;
        float zValue = -8.6f;
        int count = 0;
        foreach (int interger in levelMap2)
        {
            count += 1;
            Instantiate(wallPeices[interger], new Vector3(xValue, 0, zValue), Quaternion.Euler(90, 0, 0));
            xValue -= 0.3f;
            if (count == 14)
            {
                count = 0;
                xValue = 8f;
                zValue += 0.3f;
            }
        }

    }
}
