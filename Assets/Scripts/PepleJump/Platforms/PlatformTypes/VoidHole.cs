using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class VoidHole : IPlatform
    {
        [Zenject.Inject] private PepleJumpController pepleJumpController;

        public override void Action(Peple peple)
        {
            return;
        }

        public override PlatformType GetPlatformType()
        {
            return PlatformType.VoidHole;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.GetComponent<Peple>() != null)
            {
                collider.transform.SetParent(transform);
                collider.transform.localPosition = Vector3.zero;

                var rb = collider.GetComponent<Rigidbody2D>();

                rb.isKinematic = true;
                rb.velocity = Vector2.zero;

                pepleJumpController.OnPepleInDeadZone();
            }
        }
    }
}
