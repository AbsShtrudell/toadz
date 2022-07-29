using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameObject playAgainUI;
    [SerializeField] private TMP_Text score;
    [Zenject.Inject] private PepleJumpController pepleJumpController;
    [Zenject.Inject] private ScoreController scoreController;

    void Awake()
    {
        pepleJumpController.onGameOver += GameOver;
    }

    void GameOver()
    {
        playAgainUI.SetActive(true);
        score.text = scoreController.score.ToString();
    }

    public void Restart()
    {
        pepleJumpController.Restart();
    }
}
