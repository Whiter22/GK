using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainCharacterMovement : MonoBehaviour
{
    private CharacterController controller;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityAmount;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed;

    [Header("Stance Settings")]
    [SerializeField] private CapsuleCollider standCollider;
    //[SerializeField] private Camera standCamera;
    [SerializeField] private CapsuleCollider crouchCollider;
    //[SerializeField] private Camera crouchCamera;

    private enum PlayerStance {
        Stand,
        Crouch
    }

    private bool isSprinting;
    private float currSpeed;
    private PlayerStance currPlayerStance;
    private bool isGrounded = true;
    private Vector3 jumpVelocity;
    private float jumpCounter = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        currPlayerStance = PlayerStance.Stand;
    }

    void Update()
    {
        CalculateJump();
        CalculateMovement(); 

        CalculateStance();
    }

    void CalculateMovement () {
        currSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        float horizontalInput = 0f;
        float verticalInput = 0f;

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 forward = new Vector3(1f, 0f, 1f).normalized;
        Vector3 right = new Vector3(1f, 0f, -1f).normalized;

        Vector3 moveDirection = (right * horizontalInput + forward * verticalInput).normalized;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            
            controller.Move(moveDirection * currSpeed * Time.deltaTime);
        }
    }

    void CalculateJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            jumpCounter++;
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && jumpCounter < 2 && !isGrounded)
        {
            jumpCounter++;
            Jump();
        }
        else if(!isGrounded){
            jumpVelocity += new Vector3(0, gravityAmount, 0);
            controller.Move(jumpVelocity * Time.deltaTime);
            if(transform.position.y <= 0.04f)
            {
                isGrounded = true;
                jumpCounter = 0;
            }
        }
    }

    private void Jump()
    {
        jumpVelocity = new Vector3(0, jumpHeight, 0);
        controller.Move(jumpVelocity * Time.deltaTime);
    }

    void CalculateStance()
    {
        if (Input.GetKeyDown(KeyCode.C) && currPlayerStance == PlayerStance.Stand){
            currPlayerStance = PlayerStance.Crouch;
            controller.height = crouchCollider.height;
            controller.center = crouchCollider.center;
        }
        else if(Input.GetKeyDown(KeyCode.C) && currPlayerStance == PlayerStance.Crouch)
        {
            currPlayerStance = PlayerStance.Stand;
            controller.height = standCollider.height;
            controller.center = standCollider.center;
        }
    }
}
