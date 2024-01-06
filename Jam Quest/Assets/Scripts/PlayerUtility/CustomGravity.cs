using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float gravityScale = 1f;
    [SerializeField] float fallMultiplier = 2.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.velocity += Vector2.up * Physics2D.gravity.y * (gravityScale - 1) * Time.fixedDeltaTime;
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }
}
