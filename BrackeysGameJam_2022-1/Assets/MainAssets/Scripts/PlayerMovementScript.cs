using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    [SerializeField] private float normalMovementSpeed = 1f;
    [SerializeField] private float sprintMovementSpeed = 2f;
    [SerializeField] private float jumpHeight = 1.0f;

    [SerializeField] private float gravityMultiplier = 1f;
    
    [SerializeField] private LayerMask groundCheckLayers;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform headCheck;
    
    private CharacterController characterController;

    private Vector3 velocity;
    private bool isGrounded;
    private const float GRAVITY = -9.81f;

    private float horizontalMovement;

    private bool jump;
    private bool sprint;

    private float gravity;

    private float updatedMovementSpeed;

    private float initialStepOffset;

    private PlayerAnimationsController animationsController;
    
    private void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        initialStepOffset = characterController.stepOffset;
        
        animationsController = PlayerAnimationsController.Instance;
    }

    private void Update()
    {
        
        GetInput();
        
        UpdateGravity();  // just set the actual gravity of the player to the GRAVITY const * multiplier
        
        CheckGrounding();  // check if the character is on ground
        
        CheckHittingRoof();
        
        UpdateJumping();
        
        UpdateMovementSpeed();
        
        MovePlayer();

        UpdateOrientation();
        
        UpdateAnimations();

    }


    private void UpdateMovementSpeed()
    {
        if (sprint)
        {
            updatedMovementSpeed = sprintMovementSpeed;
        }
        else
        {
            updatedMovementSpeed = normalMovementSpeed;
        }
    }
    
    private void GetInput()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        jump = Input.GetKeyDown(KeyCode.Space);
        sprint = Input.GetKey(KeyCode.LeftShift);
    }
    
    
    private void MovePlayer()  // WASD movement
    {
        var move = new Vector3(horizontalMovement, 0f, 0f);
        characterController.Move(move * updatedMovementSpeed * Time.deltaTime);
    }
    
    
    private void UpdateJumping()  // Check if the player wants to jump and simulate gravity for the character
    {
        if (jump && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            characterController.stepOffset = 0f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
    
    
    private void CheckGrounding()  // check if the character is on ground, and if it is, set it's y velocity to a small negative number, so it doesn't levitate
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundCheckLayers);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            characterController.stepOffset = initialStepOffset;
        }
        
    }

    private void CheckHittingRoof()
    {
        var isHittingRoof = Physics.CheckSphere(headCheck.position, 0.3f, groundCheckLayers);

        if (isHittingRoof && velocity.y > 0)
        {
            velocity.y = -2f;
        }
    }


    private void UpdateOrientation()
    {
        var rightRot = Quaternion.Euler(0, 0.1f, 0);
        var leftRot = Quaternion.Euler(0, 179.9f, 0);
        if (horizontalMovement > 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rightRot, 10f);
        }
        if (horizontalMovement < 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, leftRot, 10f);
        }
        
    }
    
    private void UpdateGravity() => gravity = GRAVITY * gravityMultiplier;
    
    
    private void UpdateAnimations()
    {
        if (horizontalMovement != 0)
        {
            if (sprint && isGrounded)
            {
                // run animation
                animationsController.PlayRunAnimation();
            }
            else if(isGrounded)
            {
                // walk animation
                animationsController.PlayWalkAnimation();
            }
            else
            {
                // jump / fall
                animationsController.PlayFallAnimation();
            }
        }
        else if (isGrounded)
        {
            // idle
            animationsController.PlayIdleAnimation();
        }
        else
        {
            // jump / fall
            animationsController.PlayFallAnimation();
        }
    }
    
}
