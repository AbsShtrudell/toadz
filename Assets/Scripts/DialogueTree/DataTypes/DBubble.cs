using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShapeType
{
    None, Round, Bubble
}

[System.Serializable]
public class DBubble
{
    [SerializeField]
    public int id;
    [SerializeField]
    private string shape;
    [SerializeField]
    public string text;
    [SerializeField]
    private string trigger;
    [SerializeField]
    public int next_scene;
    [SerializeField]
    private string next_type;
    [SerializeField]
    public int next_id;

    public TriggerType triggerType
    {
        get
        {
            return EnumParser<TriggerType>.Parse(trigger);
        }
        set
        {
            trigger = value.ToString();
        }
    }

    public ShapeType shapeType
    {
        get
        {
            return EnumParser<ShapeType>.Parse(shape);
        }
        set
        {
            shape = value.ToString();
        }
    }

    public NodeType nodeType
    {
        get
        {
            return EnumParser<NodeType>.Parse(next_type);
        }
        set
        {
            next_type = value.ToString();
        }
    }
}
