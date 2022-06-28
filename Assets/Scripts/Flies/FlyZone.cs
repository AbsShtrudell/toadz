using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyZone : MonoBehaviour
{
    [Zenject.Inject] private FliesController controller;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Fly>() != null)
        {
            controller.RespawnCurrentFly();
        }
    }
}
