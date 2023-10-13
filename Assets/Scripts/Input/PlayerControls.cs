using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    #region GlobalVariables

    MyInputAction playerInputActions;
    CharacterController playerController;

    [Header("Movement")]
    public float speed = 5f;
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public bool isCrouching;
    public float standingHeight;
    public float crouchingHeight;
    public float crouchingSpeed;
    Vector3 playerVelocity;
    public bool isGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    [Header("Camera Options")]
    public Camera playerCam;
    float xRotation = 0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    #endregion

    private void Start()
    {
        playerInputActions = new MyInputAction();
        playerInputActions.Player.Enable();
        playerController = GetComponent<CharacterController>();

        standingHeight = playerController.height;
        crouchingHeight = standingHeight * 0.5f;

    }

    private void FixedUpdate()
    {
        ProcessMovement();
        ProcessCrouch();
    }

    private void Update()
    {
        isGrounded = playerController.isGrounded;
    }

    private void LateUpdate()
    {
        ProcessLook();
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            speed = sprintSpeed;
        }
        if (context.canceled)
        {
            speed = walkSpeed;
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isCrouching = true;
        }
        if (context.canceled)
        {
            isCrouching = false;
        }
    }

    private void ProcessLook()
    {
        Vector2 lookVector = playerInputActions.Player.Look.ReadValue<Vector2>();

        xRotation -= (lookVector.y * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (lookVector.x * Time.deltaTime) * xSensitivity);
    }

    public void ProcessMovement()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        Vector3 movementDirections = new Vector3(inputVector.x, 0, inputVector.y);
        playerController.Move(transform.TransformDirection(movementDirections) * speed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        playerController.Move(playerVelocity * Time.deltaTime);

    }

    public void MovementDebug(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }

    public void ProcessCrouch()
    {
        float heightChange;

        if (isCrouching == true)
        {
            heightChange = crouchingHeight;
        }
        else
        {
            heightChange = standingHeight;
        }

        if (playerController.height != heightChange)
        {
            playerController.height = Mathf.Lerp(playerController.height, heightChange, crouchingSpeed);
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Jump!" + context);
            if (isGrounded)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            }

        }
    }
}
