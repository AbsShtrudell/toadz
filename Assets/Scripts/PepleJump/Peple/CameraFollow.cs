using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 10f;
    public float targetY;

    private Vector3 targetLocation;

    void Start()
    {
        targetY = Mathf.Infinity;
    }

    void Update()
    {
        targetLocation = new Vector3(transform.position.x,
            Mathf.Min(Mathf.Max(player.position.y, transform.position.y), targetY),
            transform.position.z);
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetLocation, speed * Time.deltaTime);
    }
}
