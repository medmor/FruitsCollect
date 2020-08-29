using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    Vector3 startPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.enabled = false;
        StartCoroutine(setGravity());
        StartCoroutine(waitToReturn());
    }

    private IEnumerator setGravity()
    {
        yield return new WaitForSeconds(1);
        rb.gravityScale = 1;
    }
    private IEnumerator waitToReturn()
    {
        yield return new WaitForSeconds(5);
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 0;
        rb.rotation = 0;
        animator.enabled = true;
    }

}
