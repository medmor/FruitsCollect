using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject deathEffect;

    private float countdownSeconds;

    public void SetCountdownSeconds(float seconds)
    {
        countdownSeconds = seconds;
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        yield return new WaitForSeconds(countdownSeconds);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        if (deathEffect != null)
        {
            GameObject newObject = Instantiate(deathEffect);

            newObject.transform.position = transform.position;
        }
        SoundManager.Instance.playSound("explosionBullet");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Enemies")
        {
            print(collision.collider.tag);
            Destroy(gameObject);
        }
    }
}
