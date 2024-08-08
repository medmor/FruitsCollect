using System;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float maxJump = 16f;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = .1f;
    bool isGrounded = false;
    bool doubleJump = false;

    bool facingRight = true;
    float moveDirection = 0;
    Rigidbody2D r2d;

    Animator animator = default;


    InputActions inputActions = default;

    void Start()
    {

        inputActions = new InputActions();
        inputActions.Player.Enable();
        inputActions.Player.Jump.performed += jumpLogique;

        r2d = GetComponent<Rigidbody2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        facingRight = transform.localScale.x > 0;

        animator = GetComponent<Animator>();

    }

    void Update()
    {

        Vector2 moveVector = Vector2.zero;

        // Read the value as Vector2, which works for joystick inputs
        if (inputActions.Player.Move.controls[0].valueType == typeof(Vector2))
        {
            moveVector = inputActions.Player.Move.ReadValue<Vector2>();
        }
        else if (inputActions.Player.Move.controls[0].valueType == typeof(float))
        {
            // If it's a single axis, construct a Vector2 from the float value
            float moveAxis = inputActions.Player.Move.ReadValue<float>();
            if (moveAxis > 1)
            {
                moveAxis = 0;
            }
            else if (moveAxis < 0)
            {
                moveAxis = 1;
            }
            else if (moveAxis > 0)
            {
                moveAxis = -1;
            }
            moveVector = new Vector2(moveAxis, 0);
        }

        moveDirection = moveVector.x;

        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }

    }

    void FixedUpdate()
    {
        r2d.velocity = new Vector2(moveDirection * maxSpeed, r2d.velocity.y);
        animator.SetFloat("Speed", Math.Abs(r2d.velocity.x));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.name == "GroundCollision")
        {
            isGrounded = true;
            doubleJump = false;
            animator.SetBool("Jumping", false);
        }
    }

    private void jumpLogique(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (doubleJump && !isGrounded)
            {
                return;
            }
            SoundManager.Instance.playSound("jump");
            if (isGrounded)
            {
                isGrounded = false;
                r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
                animator.SetBool("Jumping", true);

            }
            else if (!doubleJump)
            {
                r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight * 1.2f);
                doubleJump = true;
                animator.SetBool("DoubleJump", true);
            }

        }
    }
    public void OnDoubleJumpEnds()
    {
        animator.SetBool("DoubleJump", false);
    }

    public void AppearAnimationEnd()
    {
        animator.SetTrigger("IdleBegin");
    }


}
