using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Console : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextMeshPro;
    void Start()
    {
        this.ListHREFParameters();
    }

    void ListHREFParameters()
    {
        Dictionary<string, string> prms = ParamParse.GetBrowserParameters();
        if (prms.Count == 0)
            return;

        string output = "Listing Link Parameters:\n";
        foreach (KeyValuePair<string, string> kvp in prms)
        {
            if (string.IsNullOrEmpty(kvp.Value) == true)
                output += "\tKeyword " + kvp.Key + "\n";
            else
                output += "\tMapping " + kvp.Key + " with value " + kvp.Value + "\n";
        }
        m_TextMeshPro.text = output;
    }
}