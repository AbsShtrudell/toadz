using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DBubble
{
    [SerializeField]
    public int id;
    [SerializeField]
    public int bubble_id;
    [SerializeField]
    public string text;
    [SerializeField]
    public int x_offset;
    [SerializeField]
    public int y_position;
    [SerializeField]
    public DTransition transition;
}
