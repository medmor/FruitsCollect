using UnityEngine;

public class ShootTarget : MonoBehaviour
{
    [SerializeField] private ShooterDefinition shooterDefinition;

    [SerializeField] private Transform target;

    private float timeOfLastSpawn;

    private bool targetInRange = false;

    void Start()
    {
        timeOfLastSpawn = shooterDefinition.shootRate;
    }

    void Update()
    {
        if (targetInRange)
        {
            if (Time.time >= timeOfLastSpawn + shooterDefinition.shootRate)
            {
                Vector2 actualBulletDirection = target.position - transform.position;
                GameObject newObject = Instantiate(shooterDefinition.bullet);
                newObject.transform.position = transform.position;
                newObject.transform.eulerAngles = new Vector3(0f, 0f, Utils.Angle(actualBulletDirection));
                newObject.tag = "Enemies";
                newObject.GetComponent<Bullet>().SetCountdownSeconds(shooterDefinition.bulletLifetime);

                Rigidbody2D rigidbody2D = newObject.GetComponent<Rigidbody2D>();
                if (rigidbody2D != null)
                {
                    print(shooterDefinition.fixedDirection);
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
