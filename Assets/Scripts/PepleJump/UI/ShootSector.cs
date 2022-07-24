using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSector : MonoBehaviour
{
    [Zenject.Inject] InputHandler inputHandler;

    private void Awake()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height / inputHandler.shootSector);
    }
}
