using UnityEngine;
public class Bat : Enemy
{
    public float speed;
    Transform target;
    Rigidbody2D rb;
    bool folowing;

    public float initialPositionRadius;
    Vector3 initialPosition;

    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();

        initialPosition = transform.position;

        target = GameObject.Find("Player").transform;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
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

    void FixedUpdate()
    {
        if (folowing)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, target.position, 2 * speed * Time.deltaTime));
            transform.right = new Vector3(transform.position.x - target.position.x, 0).normalized;
            animator.SetBool("Flaying", true);
        }
        else
        {
            if (Vector2.Distance(transform.position, initialPosition) > initialPositionRadius)
            {
                rb.MovePosition(Vector2.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime));
                transform.right = new Vector3(transform.position.x - initialPosition.x, 0).normalized;
            }
            else if (transform.position != initialPosition)
            {
                animator.SetBool("Flaying", false);
                //transform.position = initialPosition;
                rb.velocity = Vector2.zero;
            }
        }

    }

}
