using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class FlyingMonster : MovingHorizontallyPlatform, IMonster
    {
        [Zenject.Inject] private PepleJumpController pepleJumpController;

        void Start()
        {
            return;
        }

        void OnEnable()
        {
            float pos = Random.Range(controller.horizontalSpreadMin, controller.horizontalSpreadMax);

            positionA = Mathf.Max(pos - 1f, controller.horizontalSpreadMin);
            positionB = Mathf.Min(pos + 1f, controller.horizontalSpreadMax);

            targetX = positionA;
        }

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
            return PlatformType.FlyingMonster;
        }

        public void Die()
        {
            Despawn();
        }
    }
}