using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundsStorage
{
    private List<GameObject> backgrounds = new List<GameObject>();

    public GameObject Get(int id)
    {
        if (id >= 0 && id < backgrounds.Count)
        {
            return backgrounds[id];
        }
        return null;
    }
}
