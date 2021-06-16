using UnityEngine;

public class Bat : Enemy
{
    public float minX, maxX, minY, maxY;
    public LayerMask layer;
    public float detectionRadius = 2;
    public Transform target;
    public float speed;

    Animator animator;
    Rigidbody2D rb;

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
        if (Vector2.Distance(transform.position, target.position) < detectionRadius && inTheGrot())
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

    bool inTheGrot()
    {
        return target.position.x > minX && target.position.x < maxX && target.position.y > minY && target.position.y < maxY;
    }

}
