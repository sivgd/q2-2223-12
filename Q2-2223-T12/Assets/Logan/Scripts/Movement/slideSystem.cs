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

    [HideInInspector]
    public bool sliding;

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
        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0) && pm.state != playerMove.MovementState.crouching)
            StartSlide();

        if (Input.GetKeyUp(slideKey) && sliding)
            StopSlide();
    }

    private void FixedUpdate()
    {
        if (sliding)
            SlidingMovement();
    }

    private void StartSlide()
    {
        sliding = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        //slideTimer = maxSlideTime;
    }

    private void SlidingMovement()
    {

        rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

        //slideTimer -= Time.deltaTime;

        //if (slideTimer <= 0)
        //    StopSlide();
    }

    private void StopSlide()
    {
        sliding = false;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }

}
