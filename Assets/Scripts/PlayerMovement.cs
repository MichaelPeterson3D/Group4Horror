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

    //------------------ [Kam added]-----------------------
    [SerializeField] private AudioSource footstepsSoundEffect;
    [SerializeField] private AudioSource fastFootstepsSoundEffect;
    [SerializeField] private AudioSource heartbeatSoundEffect;

    public bool isPlayerMoving;
    //-----------------------------------------------------

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

        //------------------ [Kam added]-----------------------

        if (horizontalInput == 0 && verticalInput == 0)
        {
            footstepsSoundEffect.Stop();
            fastFootstepsSoundEffect.Stop();
            isPlayerMoving = false;
        }
        else if (!isPlayerMoving)
        {
            fastFootstepsSoundEffect.Stop();
            footstepsSoundEffect.Play();

            /*
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                fastFootstepsSoundEffect.Play();
                footstepsSoundEffect.Stop();

                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    fastFootstepsSoundEffect.Stop();
                    footstepsSoundEffect.Play();
                }
            }
            */

            isPlayerMoving = true;
        }

        //-----------------------------------------------------
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
