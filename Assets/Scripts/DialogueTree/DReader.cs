using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class DReader
{
    static public DData ReadDialogue(TextAsset file)
    {
        DData result = JsonUtility.FromJson<DData>(file.text);

        return result;
    }
}
