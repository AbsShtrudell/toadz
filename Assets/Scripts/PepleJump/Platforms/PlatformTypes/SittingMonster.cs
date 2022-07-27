using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class SittingMonster : IPlatform, IMonster
    {
        [Zenject.Inject] private PlatformsSpawner spawner;

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
            return PlatformType.SittingMonster;
        }

        public void Die()
        {
            var normalPlatform = spawner.Spawn(PlatformType.Normal);
            normalPlatform.transform.position = transform.position;

            Despawn();
        }
    }
}