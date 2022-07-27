using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class PlatformTraits : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _jumpForceNormal = 21f;
        [SerializeField, Min(0f)] private float _jumpForceSpring = 31f;
        [SerializeField, Min(0f)] private float _horizontalSpeed = 5f;
        [SerializeField, Min(0f)] private float _fallingSpeed = 5f;
        [SerializeField, Min(0f)] private float _voidGravityRadius = 1f;
        [SerializeField, Min(0f)] private float _voidGravityForce = 5f;
        [SerializeField] private Color _explosiveActiveColor = Color.red;
        [SerializeField] private Color _explosiveInactiveColor = Color.yellow;
        [SerializeField] private int _explosiveScoreLoss = 100;


        [SerializeField] private List<SpawnRule> _spawnRules;

        public float jumpForceNormal => _jumpForceNormal;
        public float jumpForceSpring => _jumpForceSpring;
        public float horizontalSpeed => _horizontalSpeed;
        public float fallingSpeed => _fallingSpeed;
        public float voidGravityRadius => _voidGravityRadius;
        public float voidGravityForce => _voidGravityForce;
        public Color explosiveActiveColor => _explosiveActiveColor;
        public Color explosiveInactiveColor => _explosiveInactiveColor;
        public int explosiveScoreLoss => _explosiveScoreLoss;
        public List<SpawnRule> spawnRules => _spawnRules;

        private void Awake()
        {
            foreach (var rule in _spawnRules)
            {
                rule.spawnChance = rule.startSpawnChance;
            }

            _spawnRules.Sort((SpawnRule sr1, SpawnRule sr2) => { 
                if (sr1.prioty == sr2.prioty) return 0; 
                if (sr1.prioty > sr2.prioty) return -1; 
                else return 1; 
            });
        }
    }

    [System.Serializable]
    public class SpawnRule
    {
        public PlatformType type;
        [Min(0)] public int maxInGame;
        [Min(0)] public int maxInRow;
        [Range(0f, 100f)] public float spawnChance;
        [Range(0f, 100f)] public float startSpawnChance;
        [Range(0f, 100f)] public float endSpawnChance;
        public float spawnChanceDelta;
        public int prioty;
    }
}