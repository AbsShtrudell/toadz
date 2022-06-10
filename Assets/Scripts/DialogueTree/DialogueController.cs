using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueController : MonoBehaviour
{
    [SerializeField]
    private TextAsset dialoguesFile;

    public DialogueNode currentNode
    { get; private set; }

    private DialoguesList nodes;

    public event Action<DialogueNode> onNodeChanged;

    private void Awake()
    {
        if (dialoguesFile != null)
        {
            nodes = DialogueReader.ReadDialogues(dialoguesFile);
            currentNode = FindNode(0);
            onNodeChanged?.Invoke(currentNode);
        }
    }

    private void OnEnable()
    {
        currentNode = FindNode(0);
    }

    private void OnDisable()
    {
        currentNode = null;
    }

    private DialogueNode FindNode(int id)
    {
        if (nodes.dialogues.Length == 0)
        {
            Debug.Log("Dialogues List Empty");
            return null;
        }

        for(int i = 0; i < nodes.dialogues.Length; i++)
        {
            if (nodes.dialogues[i].id == id) return nodes.dialogues[i];
        }
        Debug.Log("Can't Find Such Node");
        return nodes.dialogues[0];
    }

    public void NextNode(int branch)
    {
        if (currentNode == null) return;
        if(branch >= nodes.dialogues.Length) return;

        currentNode = FindNode(currentNode.branches[branch].node);

        onNodeChanged?.Invoke(currentNode);
    }
}
