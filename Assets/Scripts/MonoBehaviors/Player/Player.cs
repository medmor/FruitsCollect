using UnityEngine;


public class Player : MonoBehaviour
{

    public Transform spawnPoint = default;
    public Collected collectedPrephab = default;

    public PlayerInventory inventory = default;
    Rigidbody2D r2d;

    bool dead = false;
    Animator animator;

    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
        r2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }


    private void InstantiateCollectedPrefab(Vector3 position)
    {
        var instance = Instantiate(collectedPrephab);
        instance.transform.position = position;
    }


    public void OnDesappearEnd()
    {
        EventsManager.Instance.playerkilled?.Invoke(this);
    }

    public void Reset()
    {
        inventory.SetHearts(inventory.Hearts - 1);
        UIManager.Instance.playerInventory.SetHeartsText(inventory.Hearts);
        dead = false;
        transform.position = spawnPoint.position;
        inventory.SetHealth(inventory.maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemies" && !dead)
        {
            SoundManager.Instance.playSound("hit");
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            inventory.SetHealth(inventory.Health - enemy.enemyDefinition.damagePower);
            r2d.velocity = new Vector2(r2d.velocity.x, 3);

            if (inventory.Health < 0)
            {
                dead = true;
                animator.SetTrigger("Desappear");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collectables")
        {
            SoundManager.Instance.playSound("coin");
            InstantiateCollectedPrefab(collision.gameObject.transform.position);
            Destroy(collision.gameObject);
            inventory.CollectItem(collision.gameObject.GetComponent<Collectable>().item.id);
            if (inventory.CheckAllCollected())
            {
                EventsManager.Instance.allItemsCollected.Invoke();
            }
        }
        else if(collision.gameObject.tag == "Heart")
        {
            SoundManager.Instance.playSound("coin");
            InstantiateCollectedPrefab(collision.gameObject.transform.position);
            Destroy(collision.gameObject);
            inventory.SetHearts(inventory.Hearts + 1);
        }
    }

}




