using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsPowerup : PowerUp
{
    [SerializeField, Min(0f)] private float _multiplier = 1.5f;
    public float multiplier => _multiplier;
}
