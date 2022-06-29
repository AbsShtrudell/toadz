using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JoustController : MonoBehaviour
{
    [SerializeField, Min(1)] private int winAmount = 3;
    [SerializeField] private PowerBar bar;
    [SerializeField] private TMP_Text playerText;
    [SerializeField] private TMP_Text enemyText;
    private int playerScore = 0;
    private int enemyScore = 0;
    private bool end = false;

    public event Action onPlayerWin;
    public event Action onPlayerLose;

    void Start()
    {
        playerText.text = playerScore.ToString();
        enemyText.text = enemyScore.ToString();
    }

    public void OnPowerButtonClick()
    {
        if (end)
            return;

        if (bar.currentPower >= bar.minimalPower)
        {
            playerScore++;
            playerText.text = playerScore.ToString();
        }
        else
        {
            enemyScore++;
            enemyText.text = enemyScore.ToString();
        }

        bar.ResetPower();

        if (playerScore == winAmount)
        {
            onPlayerWin?.Invoke();
            DeinitializeJoust();
        }
        else if (enemyScore == winAmount)
        {
            onPlayerLose?.Invoke();
            DeinitializeJoust();
        }
    }

    private void DeinitializeJoust()
    {
        bar.enabled = false;
        end = true;
    }
}
