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
    [Zenject.Inject]
    private BackgroundsStorage backgroundsStorage;
    [Zenject.Inject]
    private ImagesStorage imagesStorage;

    [SerializeField]
    private RectTransform bubblesHolder;
    [SerializeField]
    private RectTransform backgroundHolder;
    [SerializeField]
    private SelectionController selectionController;

    private bool selectionMode = false;

    public void Init()
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
        Clear();
        selectionController.SetIcon1(imagesStorage.Get(selection.variants[0].icon));
        selectionController.SetIcon2(imagesStorage.Get(selection.variants[1].icon));
        selectionController.SetVariant1Text(selection.variants[0].text);
        selectionController.SetVariant2Text(selection.variants[1].text);
        ShowSelection();
    }

    private void SceneChanged(DScene scene)
    {
        Clear();
        for (int i = 0; i < backgroundsStorage.Count(); i++)
            backgroundsStorage.Get(i).gameObject.SetActive(false);
        backgroundsStorage.Get(scene.background_id).SetActive(true);
    }

    public void Clear()
    {
        for(int i = 0; i < bubblesStorage.Count(); i++)
            bubblesStorage.Get(i).gameObject.SetActive(false);
    }

    public void OnUIClicked()
    {
        if(selectionMode == false)
            dialogueController.MoveOnBubble();
    }

    public void Selected1()
    {
        dialogueController.MoveOnSelection(0);
        HideSelection();
    }

    public void Selected2()
    {
        dialogueController.MoveOnSelection(1);
        HideSelection();
    }

    public void HideSelection()
    {
        selectionMode = false;
        selectionController.gameObject.SetActive(false);
    }

    public void ShowSelection()
    {
        selectionMode = true;
        selectionController.gameObject.SetActive(true);
    }
}
