using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float radius = 3f;
    [SerializeField] private float angularSpeed = 3f;
    private SpriteRenderer sprite;
    private Vector2 currentMovement;
    private float currentSineParameter = 0f;
    private bool stop = true;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (stop)
            return;

        currentSineParameter += speed * Time.deltaTime;

        Vector3 sineVector = transform.up * Mathf.Sin(currentSineParameter * angularSpeed) * radius;
        transform.Translate(((Vector3)currentMovement * speed + sineVector) * Time.deltaTime, Space.World);
    }

    public void SetTarget(Vector2 target)
    {
        stop = false;

        currentMovement = (target - (Vector2)transform.position).normalized;
        transform.right = currentMovement;

        currentSineParameter = 0f;

        //float newX = target.x;
        //
        //if (newX > transform.position.x) while (newX > transform.position.x) newX -= 2f * Mathf.PI;
        //else while (newX < transform.position.x) newX += 2f * Mathf.PI;
        //
        //transform.position = new Vector2(newX, transform.position.y);

        sprite.flipY = currentMovement.x < 0f;
    }

    public void Stop()
    {
        stop = true;
    }
}
