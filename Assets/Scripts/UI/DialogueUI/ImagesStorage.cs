using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagesStorage
{
    private List<Sprite> speakerIcons = new List<Sprite>();
    private List<Sprite> bubbleImages = new List<Sprite>();
    private List<Sprite> selectionIcon = new List<Sprite>();

    public void Init(List<Sprite> speakers, List<Sprite> bubbles, List<Sprite> selections)
    {
        if(speakers != null)
            speakerIcons = speakers;
        if (bubbles != null)
            bubbleImages = bubbles;
        if (selections != null)
            selectionIcon = selections;
    }

    public Sprite GetSpeakerIcon(int id)
    {
        if(id >= 0 && id < speakerIcons.Count)
        {
            return speakerIcons[id];
        }
        return null;
    }

    public Sprite GetBubbleImage(int id)
    {
        if (id >= 0 && id < bubbleImages.Count)
        {
            return bubbleImages[id];
        }
        return null;
    }

    public Sprite GetSelectionIcon(int id)
    {
        if (id >= 0 && id < selectionIcon.Count)
        {
            return selectionIcon[id];
        }
        return null;
    }
}
