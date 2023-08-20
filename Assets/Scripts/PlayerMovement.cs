using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 30.0f;
    [SerializeField] private float groundDrag = 5.0f;
    [SerializeField] private Transform orientation;
    public bool canPlayerMove;
    private Vector3 moveDirection;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canPlayerMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        rb.drag = groundDrag;
        PlayerInput();
        SpeedControl();
        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 60.0f;
        }
        else
        {
            moveSpeed = 30.0f;
        }
    }
    private void FixedUpdate()
    {
        if (canPlayerMove == true)
        {
            MovePlayer();
        }
    }
    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10.0f, ForceMode.Force);
    }
    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        //limit velocity if it goes over speed
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
}
