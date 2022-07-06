using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogues.UI;
using Dialogues;

public class GMDialogue : GMTermination, IGameemode
{
    [SerializeField]
    private DialogueController dialogueController;
    [SerializeField]
    private UIController duiController;
    [SerializeField]
    private List<TextAsset> dialogues;

    private int startcode;

    public void Load(int code)
    {
        gameObject.SetActive(true);
        startcode = code;
        duiController.Init();
        if (startcode >= 0 && startcode < dialogues.Count)
            dialogueController.Init(dialogues[startcode]);
        else
            EndGamemode(3);
    }

    public void Launch()
    {
        dialogueController.Launch();
    }

    public void Stop()
    {
        duiController.Clear();
        gameObject.SetActive(false);
    }
}
