using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Dialogues
{
    namespace UI
    {
        public class SelectionController : MonoBehaviour
        {
            [SerializeField]
            private Image icon1;
            [SerializeField]
            private Image icon2;
            [SerializeField]
            private TextMeshProUGUI text1;
            [SerializeField]
            private TextMeshProUGUI text2;

            public void SetVariant1Text(string text)
            {
                text1.text = text;
            }

            public void SetVariant2Text(string text)
            {
                text2.text = text;
            }

            public void SetIcon1(Sprite sprite)
            {
                icon1.sprite = sprite;
                icon1.SetNativeSize();
            }

            public void SetIcon2(Sprite sprite)
            {
                icon2.sprite = sprite;
                icon2.SetNativeSize();
            }
        }
    }
}