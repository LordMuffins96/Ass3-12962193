using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PacManMovement : MonoBehaviour
{
    [SerializeField] private GameObject pacMan;
    private Tweener tweener;
    private List<GameObject> pacManList;
    private float stopWatch = 0f;


    void Start()
    {
        tweener = GetComponent<Tweener>();
        pacManList = new List<GameObject>();
        pacManList.Add(pacMan);
        if (tweener == null)
        {
            Debug.LogError("null");
        }
    }

    void Update()
    {
        float timer = Time.deltaTime;
        stopWatch += timer;
        if(stopWatch > 9f)
        { 
           stopWatch = 0f;
        }

        if (stopWatch > 0f && stopWatch < 0.5f)
        {
            LoopAddTween("1");
            pacMan.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        if (stopWatch > 3f && stopWatch < 3.5f)
        {
            LoopAddTween("2");
            pacMan.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
        if (stopWatch > 4.5f && stopWatch < 5f)
        {
            LoopAddTween("3");
            pacMan.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        if (stopWatch > 7.5f && stopWatch < 8f)
        {
            LoopAddTween("4");
            pacMan.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
    }


    private void LoopAddTween(string time)
    {
        bool added = false;
        foreach (GameObject item in pacManList)
        {
            if (time == "1")

                 added = tweener.AddTween(item.transform, item.transform.position, new Vector3(1.0f, -1f, 0.0f), 3f);
                 
            if (time == "3")
                 added = tweener.AddTween(item.transform, item.transform.position, new Vector3(12.0f, -5f, 0.0f), 3f);
                 
            if (time == "2")
                added = tweener.AddTween(item.transform, item.transform.position, new Vector3(1.0f, -5f, 0.0f), 1.5f);
               
            if (time == "4")
                added = tweener.AddTween(item.transform, item.transform.position, new Vector3(12.0f, -1f, 0.0f), 1.5f);

            if (added)
                break;
        }
    }
}
