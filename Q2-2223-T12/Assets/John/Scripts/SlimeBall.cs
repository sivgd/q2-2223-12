using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    private float shootForce = 10f;
    private Rigidbody rb; 
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
       // rb.AddForce(transform.forward)
    }

}
