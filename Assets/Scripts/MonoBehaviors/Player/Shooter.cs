using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField] ShooterDefinition shooterDefinition;

    [SerializeField] private InventoryItemSO bullets;

    [SerializeField] private SpriteRenderer bulletAnimationObject;

    float timeOfLastSpawn;
    InputActions inputActions;
    Vector3 bulletOffsetX = new Vector3(0.1f, 0, 0f);
    Vector3 bulletOffsetY = new Vector3(0, 0.1f, 0f);


    void Start()
    {
        timeOfLastSpawn = shooterDefinition.shootRate;
        inputActions = new InputActions();
        inputActions.Player.Fire.Enable();
        inputActions.Player.Fire.performed += context => Fire();
    }


    private bool CanFire() => Time.time >= timeOfLastSpawn + shooterDefinition.shootRate
        && bullets.GetAmount() > 0;

    void Fire()
    {
        if (!CanFire())
            return;
        StartCoroutine(WaitAnimationAndFire());

    }

    IEnumerator WaitAnimationAndFire()
    {
        while (bulletAnimationObject.color.a < 1)
        {
            bulletAnimationObject.color = new Color(bulletAnimationObject.color.r, bulletAnimationObject.color.g, bulletAnimationObject.color.b, bulletAnimationObject.color.a + .01f);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(.1f);

        bulletAnimationObject.color = new Color(bulletAnimationObject.color.r, bulletAnimationObject.color.g, bulletAnimationObject.color.b, 0f);

        Rigidbody2D rigidbody2D = SpawnBullet().GetComponent<Rigidbody2D>();

        rigidbody2D.AddForce(transform.localScale.x * Vector2.right * shooterDefinition.bulletSpeed, ForceMode2D.Impulse);

        bullets.UpdateAmount(-1);

        timeOfLastSpawn = Time.time;

        SoundManager.Instance.playSound("fire");
    }
    private GameObject SpawnBullet()
    {
        GameObject newObject = Instantiate(shooterDefinition.bullet);
        newObject.transform.localPosition = transform.position + bulletOffsetX * transform.localScale.x + bulletOffsetY;
        newObject.GetComponent<Bullet>().SetCountdownSeconds(shooterDefinition.bulletLifetime);
        return newObject;
    }

    void OnDestroy()
    {
        inputActions.Player.Fire.performed -= context => Fire();
        inputActions.Player.Fire.Disable();
    }


}
