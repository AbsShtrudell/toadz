using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialoguePerson
{
    [SerializeField]
    public int id;

    [SerializeField]
    private string emotion;

    public Emotions emotionType
    {
        get
        {
            Emotions result;
            if (!System.Enum.TryParse<Emotions>(emotion, out result))
                result = Emotions.Idle;

            return result;
        }
        set
        {
            emotion = value.ToString();
        }

    }
}
