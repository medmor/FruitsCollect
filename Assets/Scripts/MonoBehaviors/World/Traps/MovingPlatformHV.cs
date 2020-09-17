
using UnityEngine;

public class MovingPlatformHV : MonoBehaviour
{
    public float minX, maxX, minY, maxY, vX, vY;

    Rigidbody2D rb;
    void Start()
    {
        transform.position = new Vector3(minX, minY + (maxY - minY) / 2);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(vX, vY);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < minX)
        {
            rb.velocity = new Vector2(vX, rb.velocity.y);
        }
        if(transform.position.x > maxX)
        {
            rb.velocity = new Vector2(-vX, rb.velocity.y);
        }
        if(transform.position.y < minY)
        {
            rb.velocity = new Vector2(rb.velocity.x, vY);
        }
        if(transform.position.y > maxY)
        {
            rb.velocity = new Vector2(rb.velocity.x, -vY);
        }
    }
}
