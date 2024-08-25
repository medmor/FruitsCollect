using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [SerializeField] EnemySO enemyDefinition = default;
    [SerializeField] bool isAttacker = false;
    [SerializeField] Image healthBar;
    protected Animator animator = default;
    float health;


    private void Start()
    {
        animator = GetComponent<Animator>();
        health = enemyDefinition.health;
    }

    public float GetDamagePower()
    {
        return enemyDefinition.damagePower;
    }

    void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        UpdateHealthBar();
    }
    void UpdateHealthBar()
    {
        float fillPercentage = health / enemyDefinition.health;
        healthBar.fillAmount = fillPercentage;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isAttacker)
            {
                animator.SetBool("Attack", true);
            }
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(.1f);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isAttacker)
            {
                animator.SetBool("Attack", false);
            }
        }
    }

}
