using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateConstant : MonoBehaviour
{
    private Rigidbody rb;
    public float rotationSpeed; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Rotation(); 
       
    }

    void Rotation ()
    {
        rb.angularVelocity = new Vector3(0f, rotationSpeed * Time.deltaTime, 0f); 
    }
    
}
