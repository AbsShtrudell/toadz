using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueReader
{
    private DialoguesList ReadDialogues(TextAsset file)
    {
        DialoguesList result = JsonUtility.FromJson<DialoguesList>(file.text);

        return result;
    }
}
