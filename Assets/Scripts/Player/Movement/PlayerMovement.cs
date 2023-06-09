using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Ground Check")]

    [SerializeField] private float playerheight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float bufferCheckDistance = 0.1f;
    [SerializeField] private CapsuleCollider capsuleCollider;


    [Header("Movement")]

    [SerializeField] private GameObject camPos;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float groundDrag;
    [SerializeField] private Transform orientation;

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private bool isReadyToJump;
    [SerializeField] private bool isJumping;

    [Header("Keybindings")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;



    [SerializeField] private bool isCrouching;
    [SerializeField] private float crouchHeightMultiplier = 0.5f;
    [SerializeField] private float crouchMoveSpeedMultiplier = 0.5f;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        if(rb != null)
        //rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
    }
    private void Update()
    {
        GetInput();

        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        ControlSpeed();

        if (isJumping && rb.velocity.y < 0)
        {
            isJumping = false;
        }
        //transform.position = camPos.transform.position;    
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        groundCheckDistance = ((capsuleCollider.height / 2) + bufferCheckDistance);

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            if(isReadyToJump)
            {
                
                Jump();
                isJumping = true;
            }

        }
        if(Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            isJumping = false;
           
        }

        if (Input.GetKeyDown(crouchKey))
        {
            isCrouching = !isCrouching;
            if (isCrouching)
            {
                capsuleCollider.height *= crouchHeightMultiplier;
                moveSpeed *= crouchMoveSpeedMultiplier;
            }
            else
            {
                capsuleCollider.height /= crouchHeightMultiplier;
                moveSpeed /= crouchMoveSpeedMultiplier;
            }
        }
        if (isReadyToJump == false)
            Invoke(nameof(ResetJump), jumpCooldown);



        RaycastHit hit;

        if(Physics.Raycast(transform.position,transform.up,out hit, groundCheckDistance, groundLayer))
        {
            //ray hits the ground

            isGrounded = true;

         //   Debug.Log("Hit"+ " " +  hit.transform.name);
        }
        else
        {
            //ray did not hit ground
           // Debug.Log("Not hit");
            isGrounded = false;
        }

    }
    private void Move()
    {
        //calc direction

        moveDirection = orientation.forward *verticalInput + orientation.right *horizontalInput;


        if(isGrounded)
        {

            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        }

        //mid-air speed control
        else if (!isGrounded)
        {
            //Debug.Log("Here" + " is grounded :" + isGrounded);

            rb.AddForce(moveDirection.normalized * airMultiplier * moveSpeed * 10f, ForceMode.Force);
        }

    }

    private void ControlSpeed()
    {
        Vector3 flatVel = new Vector3 (rb.velocity.x, 0f, rb.velocity.z);


        if (isCrouching)
        {
            flatVel *= crouchMoveSpeedMultiplier;
        }

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isReadyToJump = false;
        isJumping = true;
    }

    private void ResetJump()
    {
        isReadyToJump = true;
        isJumping = false;
    }


}
