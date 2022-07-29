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
    [SerializeField]
    private TransitionHandler handler;

    private Web web = new Web();

    private void Awake()
    {
        StartCoroutine(web.InitGame());
    }

    void Start()
    {
        handler.StartFadeIn();

        handler.fadeInFinished += OnFadeInEnd;
        deadZone.onPepleInDeadzone += OnPepleInDeadZone;
    }

    public void OnPepleInDeadZone()
    {
        peple.StopAllCoroutines();
        handler.StartFadeOut();
        //handler.fadeOutFinished += Restart;
        onGameOver?.Invoke();

        StartCoroutine(web.EndGame(peple.GetComponent<ScoreController>().score));
    }

    private void OnWon()
    {
        handler.StartFadeOut();
        handler.fadeOutFinished += NextScene;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void NextScene()
    {
        Restart();
    }

    private void OnFadeInEnd()
    {
        peple.fade = false;
        handler.fadeInFinished -= OnFadeInEnd;
    }
}
