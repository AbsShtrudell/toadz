using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PepleScoreUI : MonoBehaviour
{
    [Zenject.Inject] private Peple peple;
    private TMP_Text text;
    private int currentScore = 0;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        currentScore = Mathf.Max(currentScore, (int)(peple.transform.position.y * 100f));

        text.text = currentScore.ToString();
    }
}
