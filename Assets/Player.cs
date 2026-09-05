using System;
using UnityEditorInternal;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    [Header("Movement Details")]
    [SerializeField] private float moveSpeed = 3.5f; //serialize field to force property to be visible in inspector
    [SerializeField] private float jumpForce = 8f;
    private float xInput;
    private bool facingRight = true;

    [Header("Collision Details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   //assigns the rigidbody component from unity (from Player) to variable
        anim = GetComponentInChildren<Animator>(); //gets child component 
    }

    private void Update()
    {
        handleCollision();
        handleInput();
        handleMovement();
        HandleAnimations();
        handleFlip();

    }




    private void HandleAnimations()
    {
        bool isMoving = rb.linearVelocity.x != 0;
        anim.SetBool("isMoving", isMoving); //sets animation bool to value of local bool
    }

    private void handleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");                          //gets the horizontal input
        if (Input.GetKeyDown(KeyCode.Space)) { Jump(); }                 //GetKey returns t when key is held down, GetKeyDown returns t when key is pressed, GetKeyUp returns t when key is release


    }
    private void handleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);               //y has to be .linearVelocity.y to make sure it stays same; if set to 0 it will resist gravity
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void handleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position,Vector2.down,groundCheckDistance,whatIsGround);
    }
    private void handleFlip()
    {
        if (rb.linearVelocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if(rb.linearVelocity.x < 0 && facingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
    }



}
