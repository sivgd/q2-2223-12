using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditsMove : MonoBehaviour
{

    public float moveSpeed = -0.02f;
    private bool paused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position -= new Vector3(0, moveSpeed, 0);

        if(Input.GetKey(KeyCode.DownArrow))
        {
            moveSpeed = -0.05f;
        }

        else if (Input.GetKey(KeyCode.UpArrow))
        {
            moveSpeed = -0.005f;
        }

        if(paused != true && Input.GetKey(KeyCode.Space))
        {
            moveSpeed = 0;
        }
        if (paused == true && Input.GetKey(KeyCode.Space))
        {
            moveSpeed = -0.02f;
        }

        else if (paused != true)
        {
            moveSpeed = -0.02f;
        }

    }
}
