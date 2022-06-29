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
    [Zenject.Inject]
    private BubblesStorage bubblesStorage;

    [SerializeField]
    private RectTransform bubblesHolder;
    [SerializeField]
    private RectTransform backgroundHolder;

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
        if (bubble == null) return;

        Bubble bubble_inst = bubblesStorage.Get(bubble.bubble_id);

        if (bubble_inst == null) return;

        bubble_inst.gameObject.SetActive(true);
        bubble_inst.SetPosition(new Vector2(bubble.x_offset, -1 * bubble.y_position * dialogueController.currentScene.bubbles_y_offset));
        bubble_inst.SetText(bubble.text);


    }

    private void SelectionSetted(DSelection selection)
    {

    }

    private void SceneChanged(DScene scene)
    {

    }

    public void Clear()
    {
        for(int i = 0; i < bubblesStorage.Count(); i++)
            bubblesStorage.Get(i).gameObject.SetActive(false);
    }

    public void OnUIClicked()
    {
        dialogueController.MoveOnBubble();
    }
}
