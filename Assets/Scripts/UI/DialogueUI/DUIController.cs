using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DUIController : MonoBehaviour
{
    [Zenject.Inject]
    private DialogueController dialogueController;
    private void OnEnable()
    {
        dialogueController.bubbleChanged += BubbleChanged;
        dialogueController.selectionSetted += SelectionSetted;
        dialogueController.sceneChanged += SceneChanged;
    }

    private void OnDisable()
    {
        dialogueController.bubbleChanged -= BubbleChanged;
        dialogueController.selectionSetted -= SelectionSetted;
        dialogueController.sceneChanged -= SceneChanged;
    }

    private void BubbleChanged(DBubble bubble)
    {

    }

    private void SelectionSetted(DSelection selection)
    {

    }

    private void SceneChanged(DScene scene)
    {

    }
}
