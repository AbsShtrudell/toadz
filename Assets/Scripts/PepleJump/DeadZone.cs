using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private PlatformController controller;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Platform")
            controller.SpawnNext(collider.transform);
        else if (collider.GetComponent<Peple>() != null)
        {
            Destroy(collider.gameObject);
        }
    }
}
