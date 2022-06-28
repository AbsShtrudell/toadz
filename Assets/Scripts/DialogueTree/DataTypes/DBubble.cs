using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DBubble
{
    [SerializeField]
    public int id;
    [SerializeField]
    private int image;
    [SerializeField]
    private int icon;
    [SerializeField]
    public string text;
    [SerializeField]
    public int x_offset;
    [SerializeField]
    public int y_offset;
    [SerializeField]
    public DTransition transition;
}
