using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    static public class DialogueReader
    {
        static public DData ReadDialogue(TextAsset file)
        {
            DData result = JsonUtility.FromJson<DData>(file.text);

            return result;
        }
    }
}
