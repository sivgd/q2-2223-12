using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float sprintSpeed;
    public float walkSpeed;
    public float slideSpeed;
    public float wallRunSpeed;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool canJump;
    private int jumpCharge = 2;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;


    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.C;


    private float horizontalInput;
    private float verticalInput;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;

    Vector3 moveDirection;

    Rigidbody rb;

    private slideSystem ss;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        wallrunning,
        crouching,
        sliding,
        air
    }

    public bool sliding;
    public bool wallrunning;

    private void Start()
    {
        ss = GetComponent<slideSystem>();

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        canJump = true;

        startYScale = transform.localScale.y;
    }

    private void FixedUpdate()
    {
        //#groundcheck!!!! slayyyyyyyyyyyyyyyyyy
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MovePlayer();

        //handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

    }

    private void Update()
    {

        if (grounded == true)
        {
            jumpCharge = 1;
        }


        MyInput();
        SpeedControl();
        StateHandler();

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //When to jump
        if (Input.GetKey(jumpKey) && canJump && (grounded || jumpCharge != 0) && state != MovementState.crouching)
        {
            canJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //start crouch
        if (Input.GetKeyDown(crouchKey) && Input.GetKey(sprintKey) == false)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        //stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }

    }

    private void StateHandler()
    {


        //Mode - Crouching
        if (Input.GetKey(crouchKey) && Input.GetKey(sprintKey) == false)
        {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed;
            
        }

        else if(wallrunning)
        {
            state = MovementState.wallrunning;
            desiredMoveSpeed = wallRunSpeed;
        }

        //Mode - Sliding
        else if (sliding)
        {
            state = MovementState.sliding;

            if (OnSlope() && rb.velocity.y < 0.1f)
                desiredMoveSpeed = slideSpeed;

            else
                desiredMoveSpeed = sprintSpeed;

        }


        //Mode - Sprinting
        else if (grounded && Input.GetKey(sprintKey) && Input.GetKeyDown(crouchKey) == false)
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
        }

        //Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
            jumpCharge = 1;
        }

        //Mode - Air
        else
        {
            state = MovementState.air;
        }

        //check if difference of desiredMoveSpeed has changed drastically

        if(Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;

    }

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        //smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while(time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);
            if (OnSlope())
            {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
            }
            else
                time += Time.deltaTime * speedIncreaseMultiplier;
            yield return null;
        }

        moveSpeed = desiredMoveSpeed;

    }

    private void MovePlayer()
    {
        //calculate movement direction
        if(sliding == false)
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        //on ground
        if(grounded)
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        //in air
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        //turn off grav while slope
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        //limiting speed on slope
        if(OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        //limiting speed on ground or air
        else
        {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        }
    }

    private void Jump()
    {

        exitingSlope = true;

        //reset y velocity

        if(jumpCharge != 0)
        {
            jumpCharge -= 1;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

    }

    private void ResetJump()
    {
        canJump = true;

        exitingSlope = false;
    }

    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }
}
