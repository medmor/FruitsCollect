
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public float minX, maxX, minY, maxY, vX, vY;
    Vector2 originalPos;

    Rigidbody2D rb;
    void Start()
    {
        originalPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(vX, vY);
    }

    // Update is called once per frame
    void Update()
    {
        var x = 0;
        var y = 0;
        if (transform.position.x < originalPos.x - minX || transform.position.x > originalPos.x + maxX)
        {
            x = -1;
        }

        if (transform.position.y < originalPos.y - minY || transform.position.y > originalPos.y + maxY)
        {
            y = -1;
        }
        if (x == 0 && y == 0)
        {
            return;
        }
        print("changing ");
        rb.velocity = new Vector2(x * rb.velocity.x, y * rb.velocity.y);

    }
}
