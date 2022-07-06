using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [System.Serializable]
    public class DVariant
    {
        [SerializeField]
        public int icon;
        [SerializeField]
        public string text;
        [SerializeField]
        public DTransition transition;
    }
}