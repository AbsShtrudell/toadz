using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRJoust : ITrigger
{
    private GMDialogue GMdialogue;

    public TRJoust(GMDialogue dialogue)
    {
        GMdialogue = dialogue;
    }

    public void Call()
    {
        GMdialogue.EndGamemode(6);
    }
}
