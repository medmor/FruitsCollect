using UnityEngine;

public class ShootTarget : MonoBehaviour
{
    [SerializeField] private ShooterDefinition shooterDefinition;

    [SerializeField] private Transform target;

    [SerializeField] private Vector2 bulletOffset = Vector2.zero;

    [SerializeField] private string attackAnimation = "";

    private float timeOfLastSpawn;

    private bool targetInRange = false;

    Animator animator;


    void Start()
    {
        timeOfLastSpawn = shooterDefinition.shootRate;
        if (target == null)
        {
            target = GameObject.Find("Player").transform;
        }
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (targetInRange)
        {
            if (Time.time >= timeOfLastSpawn + shooterDefinition.shootRate)
            {
                if (!string.IsNullOrEmpty(attackAnimation))
                {
                    animator.Play(attackAnimation);
                }

                Vector2 actualBulletDirection = target.position - transform.position;


                Rigidbody2D rigidbody2D = SpawnBullet(actualBulletDirection).GetComponent<Rigidbody2D>();

                if (rigidbody2D != null)
                {
                    if (shooterDefinition.fixedDirection != Vector2.zero)
                    {
                        rigidbody2D.AddForce(shooterDefinition.fixedDirection * shooterDefinition.bulletSpeed, ForceMode2D.Impulse);

                    }
                    else
                    {
                        rigidbody2D.AddForce(actualBulletDirection * shooterDefinition.bulletSpeed, ForceMode2D.Impulse);
                    }
                }

                timeOfLastSpawn = Time.time;

                SoundManager.Instance.playSound("fire");
            }
        }
    }
    GameObject SpawnBullet(Vector2 dir)
    {
        GameObject bullet = Instantiate(shooterDefinition.bullet);
        bullet.transform.position = transform.position + (Vector3)bulletOffset;
        bullet.transform.eulerAngles = new Vector3(0f, 0f, Utils.Angle(dir));
        bullet.tag = "Enemies";
        bullet.GetComponent<Bullet>().SetCountdownSeconds(shooterDefinition.bulletLifetime);
        return bullet;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target && collision.transform.parent == target)
        {
            targetInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (target && collision.transform.parent == target)
        {
            targetInRange = false;
        }
    }
}
