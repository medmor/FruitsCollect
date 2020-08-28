using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    Animator animator;
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
        rb.velocity = new Vector2(rb.velocity.x, 16);
    }

    public void AniamtionEnd()
    {
        animator.enabled = false;
    }
}
