using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bubble : MonoBehaviour
{
    [SerializeField]
    private RectTransform bubbleTransform;
    [SerializeField]
    private Image bubbleImage;
    [SerializeField]
    private Image bubbleIcon;
    [SerializeField]
    private TextMeshProUGUI bubbleText;

    public void SetText(string text)
    {
        bubbleText.text = text;
    }

    public void SetPosition(Vector2 position)
    {
        bubbleTransform.localPosition = new Vector3(position.x, position.y, 0);
    }
}
