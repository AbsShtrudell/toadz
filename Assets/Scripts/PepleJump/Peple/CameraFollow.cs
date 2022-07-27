using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    public float targetY;

    private Vector3 targetLocation;

    void Start()
    {
        targetY = Mathf.Infinity;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x,
            Mathf.Min(Mathf.Max(player.position.y, transform.position.y), targetY),
            transform.position.z);

            //transform.position = Vector3.Lerp(transform.position, targetLocation, 10 * Time.deltaTime);
    }
}
