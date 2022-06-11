using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OrganizationType
{
    Vertical, Horizontal
}

[System.Serializable]
public class DSelection
{
    [SerializeField]
    public int id;
    [SerializeField]
    private string organization;
    [SerializeField]
    public bool icons;
    [SerializeField]
    public DVariant[] variants;

    public OrganizationType organizationType
    {
        get
        {
            OrganizationType result;
            if (!System.Enum.TryParse<OrganizationType>(organization, out result))
                result = OrganizationType.Horizontal;

            return result;
        }
        set { organization = organizationType.ToString(); }
    }

}
