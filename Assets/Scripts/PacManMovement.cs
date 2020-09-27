using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PacManMovement : MonoBehaviour
{
    public GameObject pacMan;
    private Vector3 placement = new Vector3(3.9f, 0f, -0.3f);
    private Vector3 movementOne = new Vector3(-3.3f, 0f, 0f);
    public Animator movement;
    // Start is called before the first frame update
    void Start()
    {
        pacMan.transform.position = placement;
        pacMan.transform.rotation = Quaternion.Euler(90, 180, 0);

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("AllMovement");

    }
    IEnumerator AllMovement()
    {
        yield return FirstMovement();
    }
    IEnumerator FirstMovement()
    {
        Vector3 secondPosition = new Vector3(0.3f, 0, -0.3f);
        if (pacMan.transform.position == secondPosition)
        {
            Debug.Log("nice");
            yield return SecondMovement();

        }
        else
        {

            pacMan.transform.Translate(new Vector3(0.01f, 0.0f, 0.0f));
            Debug.Log("finish");
        }
    }
    IEnumerator SecondMovement()
    {

        yield return null;
    }
}
