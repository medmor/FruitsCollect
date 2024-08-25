using System.Collections;
using UnityEngine;

public class Turtle : Enemy
{


    Animator anim;

    bool firstHit = false;

    void Start()
    {
        anim = GetComponent<Animator>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!firstHit)
            {
                anim.Play("HitTurtle");
                anim.SetBool("Spikes", true);
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
        anim.SetBool("Spikes", false);
    }
}
