using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Emotions 
{
    Idle, Anger, Happines
}

[System.Serializable]
public struct EmotionVisual
{
    public Emotions emotion;
    public Sprite sprite;
}


public class Person : ScriptableObject
{
    [SerializeField]
    private int m_id;

    [SerializeField]
    private string m_name;

    [SerializeField]
    private List<EmotionVisual> m_Emotions = new List<EmotionVisual>(3);
}