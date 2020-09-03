using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    Vector3 startPosition;
    RigidbodyConstraints2D rbConstraints;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        rbConstraints = rb.constraints;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(setGravity());
            StartCoroutine(waitToReturn());
        }
    }

    private IEnumerator setGravity()
    {
        yield return new WaitForSeconds(.5f);
        rb.constraints = RigidbodyConstraints2D.None;
        animator.enabled = false;
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
        rb.constraints = rbConstraints;
    }

}
