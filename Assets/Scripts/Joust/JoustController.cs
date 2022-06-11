using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoustController : MonoBehaviour
{
    [SerializeField, Min(1)] private int winAmount = 3;
    [SerializeField] private PowerBar bar;
    [SerializeField] private Contestant player;
    [SerializeField] private Contestant enemy;
    private Button powerButton;

    public event Action onPlayerWin;
    public event Action onPlayerLose;

    void Start()
    {
        powerButton = bar.GetComponentInChildren<Button>();
        powerButton.onClick.AddListener(OnPowerButtonClick);

        InitializeMedals();
    }

    private void InitializeMedals()
    {
        var playerMedal = player.transform.GetChild(0).GetComponent<RectTransform>();
        var enemyMedal = enemy.transform.GetChild(0).GetComponent<RectTransform>();

        for (int i = 1; i < winAmount; i++)
        {
            var playerMedalPosition = new Vector3(playerMedal.position.x, playerMedal.position.y + playerMedal.rect.height * i, playerMedal.position.z);
            var enemyMedalPosition = new Vector3(enemyMedal.position.x, enemyMedal.position.y + enemyMedal.rect.height * i, enemyMedal.position.z);

            Instantiate(playerMedal, playerMedalPosition, Quaternion.identity, player.transform);
            Instantiate(enemyMedal, enemyMedalPosition, Quaternion.identity, enemy.transform);
        }
    }

    public void OnPowerButtonClick()
    {
        if (bar.currentPower >= bar.minimalPower)
        {
            player.WinRound();            
        }
        else
        {
            enemy.WinRound();
        }

        bar.ResetPower();

        if (player.currentWins == winAmount)
        {
            onPlayerWin?.Invoke();
            DeinitializeJoust();
        }
        else if (enemy.currentWins == winAmount)
        {
            onPlayerLose?.Invoke();
            DeinitializeJoust();
        }
    }

    private void DeinitializeJoust()
    {
        powerButton.onClick.RemoveAllListeners();
        bar.enabled = false;
    }
}
