using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DScene
{
    [SerializeField]
    public int id;
    [SerializeField]
    public int background;
    [SerializeField]
    public DBubble[] bubbles;
    [SerializeField]
    public DSelection[] selections;
}
