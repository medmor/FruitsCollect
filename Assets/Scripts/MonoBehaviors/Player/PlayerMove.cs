using System;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float maxSpeed = 3.4f;
    [SerializeField] float climbSpeed = 10f;
    [SerializeField] float jumpHeight = 6.5f;
    [SerializeField] float maxJump = 16f;

    float xInput = 0;
    bool facingRight = true;

    float yInput = 0;
    bool isLadder = false;

    bool isGrounded = false;
    bool doubleJump = false;

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
        xInput = inputActions.Player.Move.ReadValue<Vector2>().x;
        yInput = inputActions.Player.Move.ReadValue<Vector2>().y;

        if (xInput == 0)
        {
            xInput = inputActions.Player.MoveAxis.ReadValue<float>();
            xInput = xInput > 1 ? 0 : xInput > 0 ? -1 : xInput < 0 ? 1 : 0;
        }


        if (xInput != 0)
        {
            if (xInput > 0 && !facingRight)
            {
                facingRight = true;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            if (xInput < 0 && facingRight)
            {
                facingRight = false;
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }

    }

    void FixedUpdate()
    {
        r2d.AddForce(new Vector2(xInput, 0), ForceMode2D.Impulse);//.velocity = new Vector2(xInput * maxSpeed, r2d.velocity.y);
        if (xInput == 0 && Mathf.Abs(r2d.velocity.x) > float.Epsilon)
        {
            r2d.velocity = new Vector2(r2d.velocity.x / 1.1f, r2d.velocity.y);
        }
        animator.SetFloat("Speed", Math.Abs(r2d.velocity.x));

        if (Math.Abs(r2d.velocity.x) > maxSpeed)
        {
            r2d.velocity = new Vector2(Mathf.Sign(r2d.velocity.x) * maxSpeed, r2d.velocity.y);
        }

        if (r2d.velocity.y > maxJump)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, maxJump);
        }

        if (isLadder && yInput != 0)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, yInput * climbSpeed);
            animator.SetBool("Climbing", true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.name == "GroundCollision")
        {
            isGrounded = true;
            doubleJump = false;
            animator.SetBool("Jumping", false);


            animator.SetBool("Climbing", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            animator.SetBool("Climbing", false);
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

    void OnDestroy()
    {
        inputActions.Player.Jump.performed -= jumpLogique;
    }

}
