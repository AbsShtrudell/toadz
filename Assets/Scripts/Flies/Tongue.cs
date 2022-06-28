using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    [SerializeField, Min(0f)] private float forwardSpeed = 200f;
    [SerializeField, Min(0f)] private float backSpeed = 100f;
    [SerializeField, Min(0f)] private float waitTime = 0.2f;
    [SerializeField, Min(0f)] private float lickRadius = 0.2f;
    [SerializeField] private Vector2 origin;
    [SerializeField] private LayerMask flyLayer;
    [Zenject.Inject] private FliesController controller;
    private Vector2 forwardDirection;
    private Vector2 backDirection;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;

        backDirection = origin.normalized;
        forwardDirection = -backDirection;

        transform.position = origin;
    }


    public void Thrust()
    {
        if (!controller.isTongueThrusted)
        {
            sprite.enabled = true;
            controller.isTongueThrusted = true;
            StartCoroutine(Thrusting());
        }
    }

    IEnumerator Thrusting()
    {
        while (transform.localPosition != Vector3.zero)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, forwardSpeed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        yield return Waiting();
    }

    IEnumerator Waiting()
    {
        for (float waitClock = 0f; waitClock < waitTime; waitClock += Time.deltaTime)
        {
            var col = Physics2D.OverlapCircle(transform.position, lickRadius, flyLayer.value);

            if (col != null)
            {
                col.GetComponent<Fly>().Stop();
                col.transform.SetParent(transform);
                col.transform.position = transform.position;

                break;
            }

            yield return new WaitForEndOfFrame();
        }

        yield return Returning();
    }

    IEnumerator Returning()
    {
        while (transform.position != (Vector3)origin)
        {
            transform.position = Vector3.MoveTowards(transform.position, origin, backSpeed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        sprite.enabled = false;
        controller.isTongueThrusted = false;

        if (transform.childCount != 0)
            controller.SpawnNewFly();
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(origin, 0.2f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lickRadius);
    }
}
