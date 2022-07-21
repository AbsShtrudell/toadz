using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeadZone : MonoBehaviour
{
    [Zenject.Inject] private PlatformController controller;
    [Zenject.Inject] private CloudController cloudController;

    public event Action onPepleInDeadzone;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Platform>(out Platform p))
            controller.SpawnNext(p);
        else if (collider.GetComponent<Peple>() != null)
        {
            onPepleInDeadzone?.Invoke();
        }
        else if (collider.TryGetComponent<MovingCloud>(out MovingCloud cloud))
        {
            cloudController.SpawnNext(cloud);
        }
        else if (collider.TryGetComponent<ItemPickup>(out ItemPickup item))
        {
            item.ReturnToPool();
        }
    }
}
