using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class DialogueReader
{
    static public DialoguesList ReadDialogues(TextAsset file)
    {
        DialoguesList result = JsonUtility.FromJson<DialoguesList>(file.text);

        return result;
    }
}
