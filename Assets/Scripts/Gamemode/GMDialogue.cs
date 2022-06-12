using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMDialogue : GMTermination, IGameemode
{
    public void Load(int code)
    {
        gameObject.SetActive(true);
    }

    public void Launch()
    {
        EndGamemode(4);
    }

    public void Stop()
    {
        
    }
}
