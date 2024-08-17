using UnityEngine;
public class Mushroom : Enemy

{
    [SerializeField] float speed;
    Transform target;
    Rigidbody2D rb;
    bool folowing;
    Vector2 moveForce;


    void Start()
    {
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        target = GameObject.Find("Player").transform;

        moveForce = new Vector2(speed, 0);
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
            animator.SetBool("Running", false);
        }
    }

    void FixedUpdate()
    {
        if (folowing)
        {
            rb.AddForce((target.position - transform.position).normalized * moveForce, ForceMode2D.Impulse);
            //rb.MovePosition(Vector2.MoveTowards(transform.position, target.position, 2 * speed * Time.deltaTime));
            transform.right = new Vector3(transform.position.x - target.position.x, 0).normalized;
            animator.SetBool("Running", true);
        }
        else if (!folowing && Mathf.Abs(rb.velocity.x) > float.Epsilon)
        {
            rb.velocity = new Vector2(rb.velocity.x / 2f, rb.velocity.y);
        }

    }
}
