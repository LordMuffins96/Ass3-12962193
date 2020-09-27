using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManMovement : MonoBehaviour
{
    [SerializeField] private GameObject pacMan;
    private Tweener tweener;
    private List<GameObject> pacManList;


    // Use this for initialization
    void Start()
    {
        tweener = GetComponent<Tweener>();
        pacManList = new List<GameObject>();
        pacManList.Add(pacMan);
        Debug.Log(pacMan);
        Debug.Log(pacManList);
        if (tweener == null)
        {
            Debug.LogError("null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            if (pacManList[0].activeInHierarchy == false)
            {
                pacManList.Add(Instantiate(pacMan, new Vector3(-3.0f, 0.0f, -0.3f), Quaternion.identity));
            }

        if (Input.GetKeyDown("a"))
            LoopAddTween("a");
        if (Input.GetKeyDown("d"))
            LoopAddTween("d");
        if (Input.GetKeyDown("s"))
            LoopAddTween("s");
        if (Input.GetKeyDown("w"))
            LoopAddTween("w");
    }


    private void LoopAddTween(string key)
    {
        bool added = false;
        foreach (GameObject item in pacManList)
        {
            if (key == "a")
                added = tweener.AddTween(item.transform, item.transform.position, new Vector3(-2.0f, 0.5f, 0.0f), 1.5f);
            if (key == "d")
                added = tweener.AddTween(item.transform, item.transform.position, new Vector3(2.0f, 0.5f, 0.0f), 1.5f);
            if (key == "s")
                added = tweener.AddTween(item.transform, item.transform.position, new Vector3(0.0f, 0.5f, -2.0f), 0.5f);
            if (key == "w")
                added = tweener.AddTween(item.transform, item.transform.position, new Vector3(0.0f, 0.5f, 2.0f), 0.5f);

            if (added)
                break;
        }
    }
}
