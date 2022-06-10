using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueBranch
{
    [SerializeField]
    private string trigger;

    [SerializeField]
    public string text;

    [SerializeField]
    public int node;

    public TriggerTypes triggerType
    {
        get 
        {
            TriggerTypes result;
            if (!System.Enum.TryParse<TriggerTypes>(trigger, out result))
                result = TriggerTypes.None;

            return result;
        }
        set
        {
            trigger = value.ToString();
        }
            
    }
}
