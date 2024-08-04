using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


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

    // private Joystick joystick;

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

        // // joystick = UIManager.Instance.Controls.GetJoystick();
        // EventsManager.Instance.ControlsEvent.AddListener((string e) =>
        // {
        //     if (e == "Jump") jump();
        // });
    }

    void Update()
    {

        moveDirection = inputActions.Player.Move.ReadValue<Vector2>().x;

        // if (joystick.Horizontal != 0)
        // {
        //     moveDirection = joystick.Horizontal;
        // }

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
