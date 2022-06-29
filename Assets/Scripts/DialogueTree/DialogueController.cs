using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueController : MonoBehaviour
{
    [Zenject.Inject]
    private TriggersController triggersController;

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

    public void Init(TextAsset dialogueFile)
    {
        if (dialogueFile != null)
        {
            LoadDialogue(dialogueFile);

            if (dialogueData == null) return;
            SetScene(0);
        }
    }

    public void Launch()
    {
        if (dialoguesFile != null)
        {
            StartDialogue();
        }
    }

    private void OnDisable()
    {
        dialoguesFile = null;
    }

    public void LoadDialogue(TextAsset dialogue)
    {
        dialogueData = DReader.ReadDialogue(dialogue);
        dialoguesFile = dialogue;
    }

    public void StartDialogue()
    {
        if (currentScene != null)
            SetBubble(0, 0);
    }

    //-----------------------------------------------------------------------------//

    public void MoveOnSelection(int variant)
    {
        if(dialogueData == null) return;
        if(currentSelection == null) return;

        if (variant < 0 || variant >= currentSelection.variants.Length) return;

        int next_scene = currentSelection.variants[variant].transition.next_scene;
        int next_node = currentSelection.variants[variant].transition.next_id;

        triggersController.CallTrigger(currentSelection.variants[variant].transition.triggerType);

        switch (currentSelection.variants[variant].transition.nodeType)
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
    }

    public void MoveOnBubble()
    {
        if (dialogueData == null) return;
        if (currentBubble == null) return;

        int next_scene = currentBubble.transition.next_scene;
        int next_node = currentBubble.transition.next_id;

        triggersController.CallTrigger(currentBubble.transition.triggerType);

        switch (currentBubble.transition.nodeType)
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
