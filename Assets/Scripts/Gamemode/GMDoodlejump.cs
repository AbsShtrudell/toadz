using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMDoodlejump : GMTermination, IGameemode
{
    public void Launch()
    {
        gameObject.SetActive(true);
    }

    public void Load(int code)
    {
        
    }

    public void Stop()
    {
        
    }
}
