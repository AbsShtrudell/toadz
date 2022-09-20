using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [Zenject.Inject] Peple peple;

    private SafeInt _additionalScore;
    private SafeInt _heightScore;
    public int score => (int)_additionalScore + (int)_heightScore;

    public event System.Action<int> onScoreChanged;

    private void Awake()
    {
        _additionalScore = 0;
        _heightScore = 0;
    }

    private void Update()
    {
        _heightScore = Mathf.Max((int)_heightScore, (int)(peple.transform.position.y * 10f));
        
        onScoreChanged?.Invoke(score);
    }

    public void AddScore(int value)
    {
        _additionalScore += value;

        onScoreChanged?.Invoke(value);
    }
}
