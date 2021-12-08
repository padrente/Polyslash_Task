using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControler : MonoBehaviour
{
    [SerializeField] float movementSpeed = 35;
    [SerializeField] float movementSpeedAir = 0.4f;
    float horizontalMovment;
    float verticalMovement;

    float groundDrag = 6f;
    float airDrag = 2f;

    Vector3 moveDirection;

    [SerializeField] Rigidbody rb;


    //Canera control

    [SerializeField] private float sensX = 30f;
    [SerializeField] private float sensY = 30f;

    [SerializeField] Camera cam;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation;
    float yRotation;

    //jump
    bool isGrounded;
    [SerializeField] float jumpFroce;
    [SerializeField]float playerHeight = 2f;


    void Start()
    {
        rb.freezeRotation = true;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        
        IsGrounded();
        MyInput();
        ControlDrag();
        MoveCamera();

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump();
    }

    void ControlDrag()
    {
        if(isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = airDrag;
    }

    void MyInput()
    {
        horizontalMovment = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation,-90f, 90f);

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovment;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    void IsGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpFroce, ForceMode.Impulse);
    }
    void MovePlayer()
    {
        if(isGrounded)
            rb.AddForce(moveDirection.normalized * movementSpeed, ForceMode.Acceleration);
        else
            rb.AddForce(moveDirection.normalized * movementSpeed * movementSpeedAir, ForceMode.Acceleration);
    }

    void MoveCamera()
    {
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
