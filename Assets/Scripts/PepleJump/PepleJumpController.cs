using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PepleJump;

public class PepleJumpController : MonoBehaviour
{
    public event Action onGameOver;

    [SerializeField]
    private PlatformController platformController;
    [SerializeField]
    private Peple peple;
    [SerializeField]
    private DeadZone deadZone;

    private Web web = new Web();

    private void Awake()
    {
        StartCoroutine(web.InitGame());
    }

    void Start()
    {
        deadZone.onPepleInDeadzone += OnPepleInDeadZone;
    }

    public void OnPepleInDeadZone()
    {
        peple.StopAllCoroutines();
        onGameOver?.Invoke();

        StartCoroutine(web.EndGame(peple.GetComponent<ScoreController>().score));
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void NextScene()
    {
        Restart();
    }
}
