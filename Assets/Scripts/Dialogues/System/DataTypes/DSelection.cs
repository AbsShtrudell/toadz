using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [System.Serializable]
    public class DSelection
    {
        [SerializeField]
        public int id;
        [SerializeField]
        public DVariant[] variants;
    }
}