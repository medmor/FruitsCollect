using UnityEngine;

public class MovingPlatformVertical : MonoBehaviour
{
    public float minY;
    public float maxY;
    public float velocity;
    Rigidbody2D rb;
    void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, minY);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, velocity);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y > maxY)
        {
            rb.velocity = new Vector2(rb.velocity.x, -velocity);
        }
        if (transform.localPosition.y < minY)
        {
            rb.velocity = new Vector2(rb.velocity.x, velocity);
        }
    }
}
