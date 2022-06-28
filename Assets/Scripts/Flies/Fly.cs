using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [Zenject.Inject] private FliesController controller;
    private new Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    private Vector2 currentMovement;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(currentMovement * speed * Time.deltaTime, Space.World);
    }

    public void SetTarget(Vector2 target)
    {
        currentMovement = (target - (Vector2)transform.position).normalized;
        transform.right = currentMovement;

        sprite.flipY = currentMovement.x < 0f;
    }

    public void Stop()
    {
        currentMovement = Vector2.zero;
    }
}
