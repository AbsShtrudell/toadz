using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
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
            GMdialogue.EndGamemode(dialogueController.currentBubble.transition.next_scene + 7);
        }
    }
}
