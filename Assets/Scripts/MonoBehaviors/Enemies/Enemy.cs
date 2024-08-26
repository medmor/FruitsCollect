using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyDefinitionSO enemyDefinition = default;
    [SerializeField] Image healthBar;
    protected Animator animator = default;
    float health;


    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        if (enemyDefinition.isDestoryable)
        {
            health = enemyDefinition.health;
        }
    }

    public float GetDamagePower()
    {
        return enemyDefinition.damagePower;
    }

    void TakeDamage(float amount)
    {
        health -= amount;
        if (health < amount)
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
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (enemyDefinition.hasAttackAnimation)
            {
                animator.SetBool("Attack", true);
            }
        }
        else if (enemyDefinition.isDestoryable && collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(.1f);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (enemyDefinition.hasAttackAnimation)
            {
                animator.SetBool("Attack", false);
            }
        }
    }

}
