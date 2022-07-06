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
    [SerializeField] private TMP_Text playerText;
    [SerializeField] private TMP_Text enemyText;

    private int playerScore = 0;
    private int enemyScore = 0;
    private bool end = false;
    private bool buttonLocked = true;

    public event Action onPlayerWin;
    public event Action onPlayerLose;

    void Start()
    {
        playerText.text = playerScore.ToString();
        enemyText.text = enemyScore.ToString();
        Launch();
    }

    public void Launch()
    {
        slider.Launch();
        buttonLocked = false;
    }

    private void UnlockButton()
    {
        slider.onRestored -= UnlockButton;
        buttonLocked = false;
        Launch();
    }

    private void LockButton()
    {
        slider.onRestored += UnlockButton;
        buttonLocked = true;
    }

    public void OnPowerButtonClick()
    {
        if (!buttonLocked )
        {
            if (end)
                return;

            if (slider.IsBallInArea())
            {
                playerScore++;
                playerText.text = playerScore.ToString();
            }
            else
            {
                enemyScore++;
                enemyText.text = enemyScore.ToString();
            }

            LockButton();
            slider.Restore();

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
    }

    private void DeinitializeJoust()
    {
        slider.enabled = false;
        end = true;
    }
}
