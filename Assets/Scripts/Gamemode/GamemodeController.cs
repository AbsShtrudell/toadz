using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamemodeController : MonoBehaviour
{
    [Zenject.Inject]
    private GMDialogue dialogue;
    [Zenject.Inject]
    private GMDoodlejump doodlejump;
    [Zenject.Inject]
    private GMFly fly;
    [Zenject.Inject]
    private GMJoust joust;

    [Zenject.Inject]
    private TransitionHandler transitionHandler;

    private IGameemode currentGamemode;
    private IGameemode nextGamemode;
    private int nextCode;

    private void OnEnable()
    {
        dialogue.gameModeTermination += OnDialogueEnds;
        doodlejump.gameModeTermination += OnDoodlejumpEnds;
        fly.gameModeTermination += OnFlyEnds;
        joust.gameModeTermination += OnJoustEnds;

        transitionHandler.fadeOutFinished += FadeOutFinished;
        transitionHandler.fadeInFinished += FadeInFinished;

        SwitchGamemode(dialogue, 0);
    }

    private void OnDisable()
    {
        dialogue.gameModeTermination -= OnDialogueEnds;
        doodlejump.gameModeTermination -= OnDoodlejumpEnds;
        fly.gameModeTermination -= OnFlyEnds;
        joust.gameModeTermination -= OnJoustEnds;

        transitionHandler.fadeOutFinished -= FadeOutFinished;
        transitionHandler.fadeInFinished -= FadeInFinished;
    }

    private void OnJoustEnds(int code)
    {
        switch(code)
        {
            case 0:
                SwitchGamemode(dialogue, 0);
                break;
            case 1:
                SwitchGamemode(dialogue, 3);
                break;
            case 2:
                SwitchGamemode(joust, 0);
                break;
            case 3:
                ToMainMenu();
                break;
        }
    }

    private void OnFlyEnds(int code)
    {
        switch (code)
        {
            case 0:
                SwitchGamemode(dialogue, 0);
                break;
            case 1:
                SwitchGamemode(dialogue, 1);
                break;
            case 2:
                SwitchGamemode(fly, 0);
                break;
            case 3:
                ToMainMenu();
                break;
        }
    }

    private void OnDoodlejumpEnds(int code)
    {
        switch (code)
        {
            case 0:
                SwitchGamemode(dialogue, 0);
                break;
            case 1:
                SwitchGamemode(dialogue, 2);
                break;
            case 2:
                SwitchGamemode(doodlejump, 0);
                break;
            case 3:
                ToMainMenu();
                break;
        }
    }

    private void OnDialogueEnds(int code)
    {
        switch (code)
        {
            case 0:
                SwitchGamemode(dialogue, 0);
                break;
            case 1:
                SwitchGamemode(dialogue, 0);
                break;
            case 2:
                SwitchGamemode(dialogue, 0);
                break;
            case 3:
                ToMainMenu();
                break;
            case 4:
                SwitchGamemode(fly, 0);
                break;
            case 5:
                SwitchGamemode(doodlejump, 0);
                break;
            case 6:
                SwitchGamemode(joust, 0);
                break;
        }
    }

    private void ToMainMenu()
    {

    }

    private void SwitchGamemode(IGameemode gamemode, int code)
    {
        if (gamemode == null) return;

        nextGamemode = gamemode;
        nextCode = code;

        transitionHandler.StartFadeOut();
    }

    private void LoadGamemode(IGameemode gamemode, int code)
    {
        if (gamemode == null) return;

        gamemode.Load(code);
        currentGamemode = gamemode;
    }

    private void LaunchGamemode()
    {
        if(currentGamemode != null)
            currentGamemode.Launch();
    }

    private void StopGamemode()
    {
        if (currentGamemode == null) return;

        currentGamemode.Stop();
        currentGamemode = null;
    }

    private void FadeOutFinished()
    {
        StopGamemode();
        LoadGamemode(nextGamemode, nextCode);
        nextGamemode = null;
        nextCode = 0;

        transitionHandler.StartFadeIn();
    }

    private void FadeInFinished()
    {
        LaunchGamemode();
    }
}
