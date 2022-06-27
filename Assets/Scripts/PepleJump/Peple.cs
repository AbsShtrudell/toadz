using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peple : MonoBehaviour
{
    [SerializeField, Min(0f)] private float jumpForce = 10f;
    [SerializeField, Min(0f)] private float speed = 5f;
    private new Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    private float horizontalInput = 0f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            horizontalInput = Input.mousePosition.x < Screen.width / 2 ? -1f : 1f;
            
            //if (horizontalInput != 0f)
                sprite.flipX = horizontalInput > 0f;
        }
        else
            horizontalInput = 0f;

        var screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPosition.x < 0)
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 1, screenPosition.y, screenPosition.z));
        else if (screenPosition.x > Screen.width)
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0f, screenPosition.y, screenPosition.z));
    }

    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(horizontalInput * speed, rigidbody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y >= 0f)
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
    }
}
