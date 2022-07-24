using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [Zenject.Inject] Peple peple;

    private int _score = 0;
    private int _additionalScore = 0;
    private int _heightScore = 0;
    public int score => _score;

    public event System.Action<int> onScoreChanged;
    private void Update()
    {
        _heightScore = Mathf.Max(_heightScore, (int)(peple.transform.position.y * 10f));
        _score = _heightScore + _additionalScore;

        onScoreChanged?.Invoke(score);
    }

    public void AddScore(int value)
    {
        _additionalScore = value;

        onScoreChanged?.Invoke(value);
    }
}
