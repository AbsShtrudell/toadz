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

public class PersonController : MonoBehaviour
{
    [SerializeField]
    private Person[] persons;
    [SerializeField]
    private Person[] currentPerson;
    [SerializeField]
    private Sprite sprite;
}
