using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueController : MonoBehaviour
{
    [SerializeField]
    private TextAsset dialoguesFile;

    private DData dialogueData;

    public DBubble currentBubble
    { get; private set; }
    public DSelection currentSelection
    { get; private set; }
    public DScene currentScene
    { get; private set; }
    public NodeType currentNodeType
    { get; private set; }

    public event Action<DBubble> bubbleChanged;
    public event Action<DSelection> selectionSetted;
    public event Action<DScene> sceneChanged;

    private void OnEnable()
    {
        if (dialoguesFile != null)
        {
            LoadDialogue(dialoguesFile);
            StartDialogue();
        }
    }

    private void OnDisable()
    {

    }

    public void LoadDialogue(TextAsset dialogue)
    {
        dialogueData = DReader.ReadDialogue(dialogue);
        dialoguesFile = dialogue;
    }

    public void StartDialogue()
    {
        if (dialogueData == null) return;

        SetScene(0);

        if (currentScene != null)
            SetBubble(0, 0);
    }

    //-----------------------------------------------------------------------------//

    public void MoveOnSelection(int variant)
    {
        if(dialogueData == null) return;
        if(currentSelection == null) return;

        if (variant < 0 || variant >= currentSelection.variants.Length) return;

        int next_scene = currentSelection.variants[variant].next_scene;
        int next_node = currentSelection.variants[variant].next_id;

        switch (currentSelection.variants[variant].nodeType)
        {
            case NodeType.Bubble:
                if (!(currentScene.id == next_scene))
                    SetScene(next_scene);
                if (!(currentBubble.id == next_node))
                    SetBubble(next_scene, next_node);
                break;
            case NodeType.Selection:
                if (!(currentScene.id == next_scene))
                    SetScene(next_scene);
                if (!(currentBubble.id == next_node))
                    SetSelection(next_scene, next_node);
                break;
        }
        TriggersController.CallTrigger(currentSelection.variants[variant].triggerType);
    }

    public void MoveOnBubble()
    {
        if (dialogueData == null) return;
        if (currentBubble == null) return;

        int next_scene = currentBubble.next_scene;
        int next_node = currentBubble.next_id;

        switch (currentBubble.nodeType)
        {
            case NodeType.Bubble:
                if (!(currentScene.id == next_scene))
                    SetScene(next_scene);
                if (!(currentBubble.id == next_node))
                    SetBubble(next_scene, next_node);
                break;
            case NodeType.Selection:
                if (!(currentScene.id == next_scene))
                    SetScene(next_scene);
                if (!(currentBubble.id == next_node))
                    SetSelection(next_scene, next_node);
                break;
        }
        TriggersController.CallTrigger(currentBubble.triggerType);
    }

    //-----------------------------------------------------------------------------//

    private void SetBubble(int scene_id, int bubble_id)
    {
        if (dialogueData == null) return;

        currentBubble = dialogueData.FindBubble(scene_id, bubble_id);

        if(currentBubble != null)
            bubbleChanged?.Invoke(currentBubble);
    }

    private void SetSelection(int scene_id, int selection_id)
    {
        if (dialogueData == null) return;

        currentSelection = dialogueData.FindSelection(scene_id, selection_id);

        if (currentSelection != null)
            selectionSetted?.Invoke(currentSelection);
    }

    private void SetScene(int scene_id)
    {
        if (dialogueData == null) return;

        currentScene = dialogueData.FindScene(scene_id);

        if(currentScene != null)
            sceneChanged?.Invoke(currentScene);
    }
}
