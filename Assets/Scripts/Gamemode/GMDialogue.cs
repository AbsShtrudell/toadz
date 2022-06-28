using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMDialogue : GMTermination, IGameemode
{
    [SerializeField]
    private DialogueController dialogueController;
    [SerializeField]
    private DUIController duiController;


    public void Load(int code)
    {
        gameObject.SetActive(true);
    }

    public void Launch()
    {
        dialogueController.Launch(null);
    }

    public void Stop()
    {
        gameObject.SetActive(false);
    }
}
