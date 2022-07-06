using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    [SerializeField, Min(0f)] private float eyeRadius = 1f;
    [SerializeField] private Vector2 eyeOrigin; 
    [SerializeField, Min(0f)] private float lerpSpeed = 1f;
    [Zenject.Inject] private FliesController controller;

    void Start()
    {
        transform.position = eyeOrigin;
    }

    void Update()
    {
        var v = (Vector3)eyeOrigin + Vector3.ClampMagnitude(controller.fly.transform.position - (Vector3)eyeOrigin, eyeRadius);

        transform.position = Vector3.Lerp(transform.position, v, lerpSpeed * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Gizmos.DrawWireSphere(eyeOrigin, eyeRadius);
    }
}
