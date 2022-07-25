using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class PowerupTraits : MonoBehaviour
    {
        [SerializeField] private PowerupSpawnRule[] _rules;

        public PowerupSpawnRule[] rules => _rules;
    }

    [System.Serializable]
    public struct PowerupSpawnRule
    {
        public PowerUp.Type type;
        [Min(0)] public int maxInGame;
        [Min(0)] public int maxInRow;
        [Range(0, 100)] public int spawnChance; // 0 - 100
        public int prioty;
    }
}
