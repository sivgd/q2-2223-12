using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditsMove : MonoBehaviour
{

    public float moveSpeed = -0.15f;
    public bool paused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position -= new Vector3(0, moveSpeed, 0);

        if (Input.GetKeyDown(KeyCode.Space) && paused == false)
        {
            paused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && paused == true)
        {
            paused = false;
        }



        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveSpeed = -0.3f;
        }

        else if (Input.GetKey(KeyCode.UpArrow))
        {
            moveSpeed = 0.3f;
        }

        
        else if (paused != true)
        {
            moveSpeed = -0.15f;
        }
        else if (paused == true)
        {
            moveSpeed = 0f;
        }

    }
}
