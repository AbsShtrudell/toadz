using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    [SerializeField]
    public int id;

    [SerializeField]
    public string text;

    [SerializeField]
    public int background;

    [SerializeField]
    public DialoguePerson person;

    [SerializeField]
    public bool selection;

    [SerializeField]
    public DialogueBranch[] branches;
}
