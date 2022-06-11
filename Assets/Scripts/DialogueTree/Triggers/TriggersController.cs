using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
    None
}

static public class TriggersController
{
    static public void CallTrigger(TriggerType triggerType)
    {
        switch(triggerType)
        {
            case TriggerType.None: 
                break;
        }
        return;
    }
}
