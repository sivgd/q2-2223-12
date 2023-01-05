using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slideSystem : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private playerMove pm;

    [Header("Sliding")]
    //public float maxSlideTime;
    public float slideForce;
    //private float slideTimer;

    public float slideYScale;
    private float startYScale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;
    Vector3 inputDirection;

    //[HideInInspector]
    //public bool sliding;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<playerMove>();

        startYScale = playerObj.localScale.y;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(pm.sprintKey) && Input.GetKey(slideKey) && (horizontalInput != 0 || verticalInput != 0) && pm.state != playerMove.MovementState.crouching && (pm.state == playerMove.MovementState.sprinting || (pm.state == playerMove.MovementState.air && Input.GetKey(pm.sprintKey))))
            StartSlide();

        if (Input.GetKeyUp(slideKey) && pm.sliding)
            StopSlide();

        if (pm.sliding)
            SlidingMovement();

    }

    private void FixedUpdate()
    {

    }

    private void StartSlide()
    {
        pm.sliding = true;

        transform.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        

        //slideTimer = maxSlideTime;
    }

    private void SlidingMovement()
    {
        if(!pm.OnSlope() || rb.velocity.y > -0.1f)
        {
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);
        }

        else
        {
            rb.AddForce(pm.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
        }

    }

    private void StopSlide()
    {
        pm.sliding = false;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }

}
