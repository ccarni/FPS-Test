using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviour
{
    private float playerHeight = 2f;
    private PhotonView view;

    //Moving
    private float maxSpeed;
    [SerializeField] private float maxWalkSpeed;
    [SerializeField] private float maxSprintSpeed;
    [SerializeField] private float moveAcceleration = 60f;
    [SerializeField] private float airAcceleration = 0.4f;

    private Vector3 moveDirection;
    private Vector3 slopeMoveDirection;

    //Sprinting
    [SerializeField] private float walkAcceleration = 40f;
    [SerializeField] private float  sprintAcceleration = 60f;
    [SerializeField] private float speedChange = 10f;

    //Jumping
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance = 0.2f;
    private bool grounded;

    //Input
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    private float xInput, zInput;
    private bool spacebar, lShift;

    //Drag
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 2f;

    //references
    private Rigidbody rb;
    [SerializeField] private Transform orientation;

    //Slopes
    private RaycastHit slopeHit;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f)){
            if (slopeHit.normal != Vector3.up){
                return true;
            }
        }
        return false;
    }

    private void Start()
    {
        view = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if(view.IsMine){
            grounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, whatIsGround);
            GetInput();
            ControlDrag();
            SprintSpeed();
            Jump();
        }

    }

    private void FixedUpdate()
    {
        if(view.IsMine){
            Move();
        }
    }

    private void GetInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
        spacebar = Input.GetKeyDown(jumpKey);
        lShift = Input.GetKey(sprintKey);
    }

    private void Move()
    {
        moveDirection = orientation.forward * zInput + orientation.right * xInput;
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        if (grounded && !OnSlope()) {
            rb.AddForce(moveDirection.normalized * moveAcceleration, ForceMode.Acceleration);
        } else if (grounded && OnSlope()) {
            rb.AddForce(slopeMoveDirection.normalized * moveAcceleration, ForceMode.Acceleration);
        } else if (!grounded) {
            rb.AddForce(moveDirection.normalized * moveAcceleration * airAcceleration, ForceMode.Acceleration);
        }

        Vector3 velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(velocity.magnitude > maxSpeed)
            rb.velocity = Vector3.ClampMagnitude(velocity, maxSpeed) + Vector3.up * rb.velocity.y;
    }

    private void SprintSpeed()
    {
        if (Input.GetKey(sprintKey) && grounded && (zInput > 0)){
            moveAcceleration = Mathf.Lerp(moveAcceleration, sprintAcceleration, speedChange * Time.deltaTime);
            maxSpeed = maxSprintSpeed;
        } else {
            moveAcceleration = Mathf.Lerp(moveAcceleration, walkAcceleration, speedChange * Time.deltaTime);
            if(grounded)
                maxSpeed = maxWalkSpeed;
        }
    }

    private void ControlDrag()
    {
        if (grounded) {
            rb.drag = groundDrag;
        } else {
            rb.drag = airDrag;
        }
    }

    private void Jump()
    {
        if (grounded && spacebar){
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void SetSpeeds(float walk, float sprint, float acceleration, float inAirSpeed)
    {
        maxWalkSpeed = walk;
        maxSprintSpeed = sprint;
        moveAcceleration = acceleration;
        airAcceleration = inAirSpeed;
    } 
}