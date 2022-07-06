using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogues.UI;

namespace Dialogues
{
    public class TRClear : ITrigger
    {
        UIController controller;

        public TRClear(UIController controller)
        {
            this.controller = controller;
        }

        public void Call()
        {
            controller.Clear();
        }
    }
}