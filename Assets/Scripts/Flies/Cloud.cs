using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private Tongue tongue;

    void Start()
    {
        tongue = GetComponentInChildren<Tongue>();
    }

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
            tongue.Thrust();
    }
}
