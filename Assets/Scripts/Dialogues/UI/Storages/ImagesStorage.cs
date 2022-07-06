using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    namespace UI
    {
        public class ImagesStorage
        {
            private List<Sprite> selectionIcon = new List<Sprite>();

            public ImagesStorage(List<Sprite> selections)
            {
                if (selections != null)
                    selectionIcon = selections;
            }

            public Sprite Get(int id)
            {
                if (id >= 0 && id < selectionIcon.Count)
                {
                    return selectionIcon[id];
                }
                return null;
            }
        }
    }
}