using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySO enemyDefinition = default;
    [SerializeField] bool isAttacker = false;
    protected Animator animator = default;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
