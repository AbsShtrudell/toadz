using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//"max_bubbles_onscreen" : 3,
//"bubbles_offset_y" : 60,
//"buubles_offset_x" : 100,

[System.Serializable]
public class DData
{
    [SerializeField]
    public DScene[] dialogue_tree;

    public DSelection FindSelection(int sceene_id, int selection_id)
    {
        if(dialogue_tree == null) return null;

        DScene scene = FindScene(sceene_id);

        for (int i = 0; i < scene.selections.Length; i++)
        {
            if (scene.selections[i].id == selection_id) return scene.selections[i];
        }

        return null;
    }
    public DBubble FindBubble(int sceene_id, int bubble_id)
    {
        if (dialogue_tree == null) return null;

        DScene scene = FindScene(sceene_id);

        for (int i = 0; i < scene.bubbles.Length; i++)
        {
            if (scene.bubbles[i].id == bubble_id) return scene.bubbles[i];
        }

        return null;
    }
    public DScene FindScene(int sceene_id)
    {
        if (dialogue_tree == null) return null;

        for (int i = 0; i < dialogue_tree.Length; i++)
        {
            if (dialogue_tree[i].id == sceene_id) return dialogue_tree[i];
        }

        return null;
    }
}
