using UnityEngine;

public class MovingPlatformHorizontal : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float velocity;
    Rigidbody2D rb;
    void Start()
    {
        transform.localPosition = new Vector3(minX, transform.localPosition.y);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(velocity, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x > maxX)
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
        }
        if (transform.localPosition.x < minX)
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
        }
    }
}
