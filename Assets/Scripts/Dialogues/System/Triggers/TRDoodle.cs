using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public class TRDoodle : ITrigger
    {
        private GMDialogue GMdialogue;

        public TRDoodle(GMDialogue dialogue)
        {
            GMdialogue = dialogue;
        }

        public void Call()
        {
            GMdialogue.EndGamemode(5);
        }
    }
}