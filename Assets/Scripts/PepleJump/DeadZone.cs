using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour
{
    [Zenject.Inject] private PlatformController controller;
    [Zenject.Inject] private CloudController cloudController;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Platform>(out Platform p))
            controller.SpawnNext(p);
        else if (collider.GetComponent<Peple>() != null)
        {
            //Destroy(collider.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (collider.TryGetComponent<MovingCloud>(out MovingCloud cloud))
        {
            cloudController.SpawnNext(cloud);
        }
    }
}
