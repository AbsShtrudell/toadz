using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    public float targetY;

    void Start()
    {
        targetY = Mathf.Infinity;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            Mathf.Min(Mathf.Max(player.position.y, transform.position.y), targetY),
            transform.position.z);
    }
}
