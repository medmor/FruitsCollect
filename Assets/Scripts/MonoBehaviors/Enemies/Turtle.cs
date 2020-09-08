using System.Collections;
using UnityEngine;

public class Turtle : Enemy
{

    public Sprite withSpikes, withoutSpikes;
    public float minX, maxX, velocity;

    Rigidbody2D rb;
    Animator animator;

    bool firstHit = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(velocity, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x > maxX)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
        }
        if (transform.localPosition.x < minX)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            rb.velocity = new Vector2(velocity, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!firstHit) {
                animator.Play("HitTurtle");
                animator.SetBool("Spikes", true);
                firstHit = true;
            }
            else
            {
                SoundManager.Instance.playSound("hit");
            }

        }
    }

    public void HitAnimationEnd() {
        gameObject.tag = "Enemies";
        StartCoroutine(HideSpikesCoroutine());
    }

    public void SpikesInAnimationEnd()
    {
        firstHit = false;
        gameObject.tag = "Untagged";
    }

    private IEnumerator HideSpikesCoroutine()
    {
        yield return new WaitForSeconds(5);
        animator.SetBool("Spikes", false);
    }
}
