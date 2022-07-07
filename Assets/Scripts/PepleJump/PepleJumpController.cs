using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void Start()
    {
        handler.StartFadeIn();

        handler.fadeInFinished += OnFadeInEnd;
        deadZone.onPepleInDeadzone += OnPepleInDeadZone;
        platformController.onPepleWon += OnWon;
    }

    private void OnPepleInDeadZone()
    {
        handler.StartFadeOut();
        handler.fadeOutFinished += Restart;
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
        SceneManager.LoadScene("Joust");
    }

    private void OnFadeInEnd()
    {
        peple.fade = false;
        handler.fadeInFinished -= OnFadeInEnd;
    }
}
