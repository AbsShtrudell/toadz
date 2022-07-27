using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class SittingMonster : IPlatform, IMonster
    {
        [Zenject.Inject] private PlatformsSpawner spawner;
        [Zenject.Inject] private PepleJumpController pepleJumpController;

        public override void Action(Peple peple)
        {
            return;
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

                peple.transform.SetParent(transform);
                peple.transform.localPosition = Vector3.zero;
                peple.isDead = true;

                var rb = peple.GetComponent<Rigidbody2D>();
                rb.isKinematic = true;
                rb.velocity = Vector2.zero;

                pepleJumpController.OnPepleInDeadZone();
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