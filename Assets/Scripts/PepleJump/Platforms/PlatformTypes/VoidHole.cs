using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class VoidHole : IPlatform
    {
        [Zenject.Inject] private PepleJumpController pepleJumpController;
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
                collider.transform.SetParent(transform);
                collider.transform.localPosition = Vector3.zero;
                collider.GetComponent<Peple>().isDead = true;
                var rb = collider.GetComponent<Rigidbody2D>();

                rb.isKinematic = true;
                rb.velocity = Vector2.zero;

                pepleJumpController.OnPepleInDeadZone();
            }
        }

        void Update()
        {
            //    if (peple.transform.parent != null) return;

            //    Vector2 vec = transform.position - peple.transform.position;
            //    float magnitude = vec.magnitude;

            //    if (magnitude <= traits.voidGravityRadius)
            //    {
            //        float newMagnitude = traits.voidGravityRadius - magnitude;

            //        peple.transform.Translate(vec.normalized * newMagnitude * traits.voidGravityForce * Time.deltaTime);
            //    }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, 1);
        }
    }
}
