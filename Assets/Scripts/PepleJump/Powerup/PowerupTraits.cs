using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class PowerupTraits : MonoBehaviour
    {
        [SerializeField] private PowerupSpawnRule[] _rules;
        [SerializeField, Min(0)] public int maxInGame;

        public PowerupSpawnRule[] rules => _rules;
    }

    [System.Serializable]
    public struct PowerupSpawnRule
    {
        public PowerUp.Type type;
        [Range(0, 100)] public int spawnChance;
    }
}
