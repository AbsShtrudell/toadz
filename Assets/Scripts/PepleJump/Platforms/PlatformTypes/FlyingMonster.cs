using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class FlyingMonster : MovingHorizontallyPlatform, IMonster
    {
        [Zenject.Inject] private ScoreController scoreController;

        private bool Dead = false;

        void Start()
        {
            return;
        }

        void OnEnable()
        {
            float pos = Random.Range(controller.horizontalSpreadMin + 1f, controller.horizontalSpreadMax - 1f);

            positionA = pos - 1f;
            positionB = pos + 1f;

            targetX = positionA;

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

        protected override void Update()
        {
            if(!Dead)
                base.Update();
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
            return PlatformType.FlyingMonster;
        }

        public void Die()
        {
            if (Dead) return;

            scoreController.AddScore(traits.monsterScoreBonus);

            Animator animator;
            if (TryGetComponent<Animator>(out animator))
                animator.SetTrigger("Die");

            foreach(var collider in GetComponents<BoxCollider>())
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