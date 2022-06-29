using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
    None, Clear, Fly, Joust, Doodle, End
}

public class TriggersController
{
    private List<ITrigger> triggers;

    public TriggersController(List<ITrigger> triggers)
    {
        this.triggers = triggers;
    }

    public void CallTrigger(TriggerType triggerType)
    {
        switch(triggerType)
        {
            case TriggerType.None: 
                break;
            case TriggerType.Clear:
                triggers[0].Call();
                break;
            case TriggerType.Fly:
                triggers[1].Call();
                break;
            case TriggerType.Joust:
                triggers[2].Call();
                break;
            case TriggerType.Doodle:
                triggers[3].Call();
                break;
            case TriggerType.End:
                triggers[4].Call();
                break;
        }
        return;
    }
}
