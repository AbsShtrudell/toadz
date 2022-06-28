using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DScene
{
    [SerializeField]
    public int id;
    [SerializeField]
    public int background_id;
    [SerializeField]
    public int bubbles_count;
    [SerializeField]
    public int bubbles_y_offset;
    [SerializeField]
    public DBubble[] bubbles;
    [SerializeField]
    public DSelection[] selections;
}
