using UnityEngine;

public class Bat : Enemy
{
    public Transform target;
    public float speed;
    Animator animator;
    Rigidbody2D rb;
    bool folowing;

    public float initialPositionRadius;
    Vector3 initialPosition;

    void Start()
    {
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        initialPosition = transform.position;

        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (folowing)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, target.position, 2 * speed * Time.deltaTime));
            animator.SetBool("Flaying", true);
        }
        else if (Vector2.Distance(transform.position, initialPosition) > initialPositionRadius)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime));
        }
        else if (Vector2.Distance(transform.position, initialPosition) < initialPositionRadius)
        {
            animator.SetBool("Flaying", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            folowing = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            folowing = false;
        }
    }

}
