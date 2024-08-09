using System.Collections;
using UnityEngine;

public class Turtle : Enemy
{


    Animator animator;

    bool firstHit = false;

    void Start()
    {
        animator = GetComponent<Animator>();


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!firstHit)
            {
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

    public void HitAnimationEnd()
    {
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
