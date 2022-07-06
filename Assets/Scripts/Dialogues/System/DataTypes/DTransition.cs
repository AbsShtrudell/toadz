using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public enum NodeType
    {
        Bubble, Selection
    }


    [System.Serializable]
    public class DTransition
    {
        [SerializeField]
        private string trigger;
        [SerializeField]
        public int next_scene;
        [SerializeField]
        private string next_type;
        [SerializeField]
        public int next_id;

        public TriggerType triggerType
        {
            get
            {
                TriggerType result;
                if (!System.Enum.TryParse<TriggerType>(trigger, out result))
                    result = TriggerType.None;

                return result;
            }
            set
            {
                trigger = value.ToString();
            }
        }

        public NodeType nodeType
        {
            get
            {
                NodeType result;
                if (!System.Enum.TryParse<NodeType>(next_type, out result))
                    result = NodeType.Bubble;

                return result;
            }
            set
            {
                next_type = value.ToString();
            }
        }
    }
}