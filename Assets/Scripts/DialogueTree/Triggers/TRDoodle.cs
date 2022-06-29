using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
