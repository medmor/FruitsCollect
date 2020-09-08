using UnityEngine;

public class Trampoline : MonoBehaviour
{
    Animator animator;
    public float jumpForce = 16;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.enabled = true;
        animator.Play("Trampoline");
        var rb = collision.gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void AniamtionEnd()
    {
        animator.enabled = false;
    }
}
