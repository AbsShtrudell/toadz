using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PepleScoreUI : MonoBehaviour
{
    [Zenject.Inject] private ScoreController score;

    private TMP_Text text;

    private void Awake()
    {
        score.onScoreChanged += ChangeTetx;
    }

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void ChangeTetx(int value)
    {
        text.text = value.ToString();
    }
}
