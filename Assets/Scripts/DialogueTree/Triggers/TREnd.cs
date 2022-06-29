using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TREnd : ITrigger
{
    private GMDialogue GMdialogue;
    private DialogueController dialogueController;

    public TREnd(GMDialogue dialogue, DialogueController dialogueController)
    {
        GMdialogue = dialogue;
        this.dialogueController = dialogueController;
    }

    public void Call()
    {
        GMdialogue.EndGamemode(4);
    }
}
