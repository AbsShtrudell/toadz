using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class VoidHole : IPlatform
    {
        [Zenject.Inject] private Peple peple;

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
                peple.Die(transform);
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, 1);
        }
    }
}
