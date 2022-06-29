using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRFly : ITrigger
{
    private GMDialogue GMdialogue;

    public TRFly(GMDialogue dialogue)
    {
        GMdialogue = dialogue;
    }

    public void Call()
    {
        GMdialogue.EndGamemode(4);
    }
}
