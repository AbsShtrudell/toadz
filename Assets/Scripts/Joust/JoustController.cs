using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JoustController : MonoBehaviour
{
    [SerializeField, Min(1)] private int winAmount = 3;
    [SerializeField] private Slider slider;
    [SerializeField] private Enemy enemy;
    [SerializeField] private TMP_Text playerText;
    [SerializeField] private TMP_Text enemyText;

    private int playerScore = 0;
    private int enemyScore = 0;

    private bool buttonLocked = true;

    public event Action onPlayerWin;
    public event Action onPlayerLose;

    void Start()
    {
        playerText.text = playerScore.ToString();
        enemyText.text = enemyScore.ToString();
    }

    private void OnEnable()
    {
        enemy.onRaundPlayed += EnemyResults;

        Launch();
    }

    private void OnDisable()
    {
        enemy.onRaundPlayed -= EnemyResults;
    }

    public void Launch()
    {
        slider.Launch();
        UnlockButton();
    }

    private void StartEnemyRound()
    {
        slider.onRestored -= StartEnemyRound;
        enemy.PlayRound();
    }

    private void EnemyResults(bool win)
    {
        enemyScore += win ? 1 : 0;
        enemyText.text = enemyScore.ToString();

        if (enemyScore == winAmount)
        {
            EndGame(false);
            return;
        }

        UnlockButton();
        Launch();
    }

    private void PlayerResults(bool win)
    {
        playerScore += win ? 1 : 0;
        playerText.text = playerScore.ToString();

        slider.Restore();
        LockButton();

        if (playerScore == winAmount)
        {
            EndGame(true);
            return;
        }

        slider.onRestored += StartEnemyRound;
    }

    private void EndGame(bool win)
    {
        LockButton();
        if (win) onPlayerWin?.Invoke();
        else onPlayerLose?.Invoke();
    }

    public void OnPowerButtonClick()
    {
        if (!buttonLocked)
        {
            if (slider.IsBallInArea())
            {
                PlayerResults(true);
            }
            else PlayerResults(false);
        }
    }

    private void UnlockButton()
    {
        buttonLocked = false;
    }

    private void LockButton()
    {
        buttonLocked = true;
    }
}