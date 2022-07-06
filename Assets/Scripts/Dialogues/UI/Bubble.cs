using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Febucci.UI;

namespace Dialogues
{
    namespace UI
    {
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

            public bool IsTyping
            { get; private set; }

            public void SetText(string text)
            {
                bubbleText.text = text;
            }

            public void SkipTyping()
            {
                GetComponentInChildren<TextAnimatorPlayer>().SkipTypewriter();
            }

            public void OnTypingStarts()
            {
                IsTyping = true;
            }

            public void OnTypingEnds()
            {
                IsTyping = false;
            }

            public void SetPosition(Vector2 position)
            {
                bubbleTransform.localPosition = new Vector3(position.x, position.y, 0);
            }
        }
    }
}