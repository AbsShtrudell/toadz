using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PepleJump;

public class PepleJumpController : MonoBehaviour
{
    [SerializeField]
    private PlatformController platformController;
    [SerializeField]
    private Peple peple;
    [SerializeField]
    private DeadZone deadZone;
    [SerializeField]
    private TransitionHandler handler;

    private bool restart = false;

    private Web web = new Web();

    private void Awake()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            StartCoroutine(web.InitGame(1234));
        #endif
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
        handler.fadeOutFinished += Restart;

        #if UNITY_WEBGL && !UNITY_EDITOR
            StartCoroutine(web.EndGame(1234, 1222));
        #endif
    }

    private void OnWon()
    {
        handler.StartFadeOut();
        handler.fadeOutFinished += NextScene;
    }

    private void Restart()
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
