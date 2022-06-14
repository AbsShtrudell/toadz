using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [Zenject.Inject] private PlatformController controller;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Platform>(out Platform p))
            controller.SpawnNext(p);
        else if (collider.GetComponent<Peple>() != null)
        {
            Destroy(collider.gameObject);
        }
    }
}
