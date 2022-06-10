using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueBranch
{
    [SerializeField]
    private TriggerTypes trigger;
    [SerializeField]
    private string text;
    [SerializeField]
    private int node;
}
