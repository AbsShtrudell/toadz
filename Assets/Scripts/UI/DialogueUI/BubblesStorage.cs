using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesStorage
{
    private List<Bubble> bubbles = new List<Bubble>();

    public BubblesStorage(List<Bubble> bubbles)
    {
        if (bubbles != null)
            this.bubbles = bubbles;
    }


    public Bubble Get(int id)
    {
        if (id >= 0 && id < bubbles.Count)
        {
            return bubbles[id];
        }
        return null;
    }
}
