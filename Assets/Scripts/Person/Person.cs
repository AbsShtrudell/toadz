using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PersonSO", menuName = "ScriptableObjects/Person")]
public class Person : ScriptableObject
{
    [SerializeField]
    private int m_id;

    [SerializeField]
    private string m_name;

    [SerializeField]
    private List<EmotionVisual> m_emotions;

    public int id
    { get { return m_id; } private set { m_id = value; } }

    public string personName
    { get { return m_name; } private set { m_name = value; } }

    public List<EmotionVisual> emotions
    { get { return m_emotions; } private set { m_emotions = value; } }
}
