using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class FlyingMonster : MovingHorizontallyPlatform, IMonster
    {
        [Zenject.Inject] private ScoreController scoreController;

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
        }

        public override void Action(Peple peple)
        {
            peple.JumpImmediately(traits.jumpForceNormal);
            Die();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent<Peple>(out var peple))
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
            scoreController.AddScore(traits.monsterScoreBonus);
            Despawn();
        }
    }
}