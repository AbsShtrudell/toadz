using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class SittingMonster : IPlatform, IMonster
    {
        [Zenject.Inject] private ScoreController scoreController;
        [Zenject.Inject] private PlatformsSpawner spawner;

        [SerializeField] Transform spawnPlatform;

        private bool Dead = false;

        private void OnEnable()
        {
            Dead = false;

            foreach (var collider in GetComponents<BoxCollider>())
            {
                collider.enabled = true;
            }
        }

        private void OnDisable()
        {
            Dead = false;

            foreach (var collider in GetComponents<BoxCollider>())
            {
                collider.enabled = true;
            }
        }

        public override void Action(Peple peple)
        {
            peple.JumpImmediately(traits.jumpForceNormal);
            Die();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent<Peple>(out var peple) && !Dead)
            {
                if (peple.gameObject.layer == LayerMask.NameToLayer("Invincible"))
                {
                    Die();
                    return;
                }

                peple.Die(transform);
            }
        }

        public override PlatformType GetPlatformType()
        {
            return PlatformType.SittingMonster;
        }

        public void Die()
        {
            if (Dead) return;

            var normalPlatform = spawner.Spawn(PlatformType.Normal);
            normalPlatform.transform.position = spawnPlatform.position;

            Animator animator;
            if (TryGetComponent<Animator>(out animator)) 
                animator.SetTrigger("Die");

            scoreController.AddScore(traits.monsterScoreBonus);

            foreach (var collider in GetComponents<BoxCollider>())
            {
                collider.enabled = false;
            }

            Dead = true;
        }

        public bool isDead()
        {
            return Dead;
        }
    }
}