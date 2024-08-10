using UnityEngine;


public class Player : MonoBehaviour
{

    //public Transform spawnPoint = default;
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

        //the inventoryUI is not destroyed and keep track of player hearts
        //thats a simple not correct to keep the player and the inventory synchronized
        inventory.SetHearts(UIManager.Instance.PlayerInventory.GetHeartsInt());
        inventory.SetHealth(UIManager.Instance.PlayerInventory.GetHealth());

    }


    private void InstantiateCollectedPrefab(Vector3 position)
    {
        var instance = Instantiate(collectedPrephab);
        instance.transform.position = position;
    }


    public void OnDesappearEnd()
    {
        Die();
        Reset();
        if (inventory.Hearts <= 0)
            EventsManager.Instance.Playerkilled?.Invoke(this);
    }

    public void Reset()
    {
        dead = false;
        transform.position = GameObject.Find(GameManager.Instance.GameSettings.currentLevelName + "(Clone)/GamePoints/SpawnPoint").transform.position;
    }
    public void Die()
    {
        inventory.SetHearts(inventory.Hearts - 1);
        inventory.SetHealth(inventory.maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemies" && !dead)
        {
            OnEnemyHit(collision.gameObject.GetComponent<Enemy>());
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemies" && !dead)
        {
            OnEnemyHit(collision.gameObject.GetComponent<Enemy>());
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
                EventsManager.Instance.AllItemsCollected.Invoke();
            }
        }
        else if (collision.gameObject.tag == "Heart")
        {
            SoundManager.Instance.playSound("coin");
            InstantiateCollectedPrefab(collision.gameObject.transform.position);
            Destroy(collision.gameObject);
            inventory.SetHearts(inventory.Hearts + 1);
        }
    }

    void OnEnemyHit(Enemy enemy)
    {
        SoundManager.Instance.playSound("hit");
        inventory.SetHealth(inventory.Health - enemy.enemyDefinition.damagePower);
        r2d.velocity = new Vector2(r2d.velocity.x, 3);

        if (inventory.Health <= 0)
        {
            dead = true;
            animator.SetTrigger("Desappear");
        }
    }

}




