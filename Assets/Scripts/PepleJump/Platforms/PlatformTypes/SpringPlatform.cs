using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class SpringPlatform : IPlatform
    {
        public override void Action(Peple peple)
        {
            if (Mathf.Abs(transform.position.x - peple.transform.position.x) < 0.45f)
                peple.Jump(traits.jumpForceSpring, transform);
            else
                peple.Jump(traits.jumpForceNormal, transform);
        }

        public override PlatformType GetPlatformType()
        {
            return PlatformType.Spring;
        }
    }
}