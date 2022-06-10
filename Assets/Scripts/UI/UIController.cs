using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private int selectionButtonsCount = 4;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI dialogueText;
    [SerializeField]
    private RectTransform TextPanel;
    [SerializeField]
    private RectTransform SelectionButtonsPanel;
    [SerializeField]
    private GameObject SelectionButtonRef;

    [SerializeField]
    private DialogueController dialogueController;

    private bool select;
    private List<RectTransform> buttons = new List<RectTransform>();

    private void Awake()
    {
        InitializeButtons();
    }

    private void OnEnable()
    {
        dialogueController.onNodeChanged += DialogueController_onNodeChanged;
    }

    private void OnDisable()
    {
        dialogueController.onNodeChanged -= DialogueController_onNodeChanged;
    }

    private void DialogueController_onNodeChanged(DialogueNode node)
    {
        ChangeDialogueText(node.text);
        ChangeDialoguePanelState(node.branches, node.selection);
    }

    public void onTextPanelClicked()
    {
        if (select) return;

        dialogueController.NextNode(0);
    }

    public void onSelectButtonClicked(int branch)
    {
        dialogueController.NextNode(branch);
    }

    public void ChangeNameText(string name)
    {
        nameText.text = name;
    }

    public void ChangeDialogueText(string text)
    {
        dialogueText.text = text;
    }

    private void ChangeDialoguePanelState(DialogueBranch[] branches, bool selection)
    {
        select = selection;

        if(selection)
        {
            int count = branches.Length;

            SelectionButtonsPanel.gameObject.SetActive(true);

            TextPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 250f - (count * SelectionButtonRef.GetComponent<RectTransform>().sizeDelta.y + (count + 1) * 5));

            for (int i = 0; i < count && i < buttons.Count; i++)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = branches[i].text;
            }
        }
        else
        {
            for (int i = 0; i < buttons.Count; i++)
                buttons[i].gameObject.SetActive(false);

            SelectionButtonsPanel.gameObject.SetActive(false);
            TextPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 410f);

        }
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < selectionButtonsCount; i++)
        {
            int index = i;
            RectTransform button = GameObject.Instantiate(SelectionButtonRef, SelectionButtonsPanel).GetComponent<RectTransform>();
            buttons.Add(button);
            button.GetComponent<Button>().onClick.AddListener(() => onSelectButtonClicked(index));
        }
    }
}
