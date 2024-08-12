﻿using UnityEngine;

public class MovingPlatformHorizontal : MonoBehaviour
{
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float velocity;
    [SerializeField] bool afterPlayerIn = false;
    [SerializeField] bool startOnMax = false;
    bool isPlayerIn;
    Rigidbody2D rb;
    void Start()
    {
        resetPosition();

        rb = GetComponent<Rigidbody2D>();
        if (afterPlayerIn)
        {
            return;
        }
        rb.velocity = new Vector2(velocity, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (afterPlayerIn && !isPlayerIn)
        {
            return;
        }
        if (transform.localPosition.x > maxX)
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
        }
        if (transform.localPosition.x < minX)
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isPlayerIn = true;
        rb.velocity = new Vector2(velocity, rb.velocity.y);
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        isPlayerIn = false;
        rb.velocity = Vector2.zero;
        resetPosition();
    }

    void resetPosition()
    {
        if (startOnMax)
        {
            transform.localPosition = new Vector3(maxX, transform.localPosition.y);
        }
        else
        {
            transform.localPosition = new Vector3(minX, transform.localPosition.y);
        }
    }
}
