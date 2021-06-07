using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerMove : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float maxJump = 16f;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 1;
    bool isGrounded = false;
    bool doubleJump = false;

    bool facingRight = true;
    float moveDirection = 0;
    Rigidbody2D r2d;
    Transform t;

    Animator animator = default;

    private Joystick joystick;

    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        facingRight = t.localScale.x > 0;

        animator = GetComponent<Animator>();

        joystick = UIManager.Instance.Controls.GetJoystick();
        EventsManager.Instance.ControlsEvent.AddListener((string e) =>
        {
            if (e == "Jump") jump();
        });
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = Input.GetKey(KeyCode.LeftArrow) ? -1 : 1;
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }
        }

        if (joystick.Horizontal != 0)
        {
            moveDirection = joystick.Horizontal;
        }

        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        jumpLogique();

    }

    void FixedUpdate()
    {
        r2d.velocity = new Vector2(moveDirection * maxSpeed, r2d.velocity.y > maxJump ? jumpHeight : r2d.velocity.y);
        animator.SetFloat("Speed", Math.Abs(r2d.velocity.x));
    }

    private void jumpLogique()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                doubleJump = false;
                animator.SetBool("Jumping", false);
                break;
            }
        }

        if (colliders.Length == 1)
        {
            animator.SetBool("Jumping", true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();

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

    private void jump()
    {
        SoundManager.Instance.playSound("jump");
        if (isGrounded)
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        else if (!doubleJump)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight * 1.2f);
            doubleJump = true;
            animator.SetBool("DoubleJump", true);
        }

    }



}
