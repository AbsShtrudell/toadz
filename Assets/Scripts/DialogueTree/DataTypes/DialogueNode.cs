using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    [SerializeField]
    private int id;
    [SerializeField]
    private string text;
    [SerializeField]
    private int background;
    [SerializeField]
    private DialoguePerson[] persons;
    [SerializeField]
    private bool selection;
    [SerializeField]
    private DialogueBranch[] branches;
}
