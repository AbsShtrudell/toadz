using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarFillUI : MonoBehaviour
{
    [Zenject.Inject] private FliesController controller;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        image.fillAmount = 0f;

        controller.onFliesCountChange += OnFliesCountChange;
    }

    void OnFliesCountChange()
    {
        image.fillAmount = 1f - (controller.currentFlies + 1f) / controller.flyCount;
    }
}
