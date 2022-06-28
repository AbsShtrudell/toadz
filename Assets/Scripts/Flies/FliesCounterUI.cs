using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FliesCounterUI : MonoBehaviour
{
    [Zenject.Inject] private FliesController controller;
    private TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        controller.onFliesCountChange += OnFliesCountChange;
    }

    void OnFliesCountChange()
    {
        text.text = (controller.currentFlies + 1).ToString();
    }
}
