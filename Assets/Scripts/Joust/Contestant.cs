using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contestant : MonoBehaviour
{
    private int _currentWins = 0;

    public int currentWins => _currentWins;

    public void WinRound()
    {
        transform.GetChild(_currentWins).GetComponent<Image>().color = Color.white;

        _currentWins++;
    }
}
