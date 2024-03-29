using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class SpringPlatform : IPlatform
    {
        public override void Action(Peple peple)
        {
            peple.Jump(traits.jumpForceSpring, transform);
        }

        public override PlatformType GetPlatformType()
        {
            return PlatformType.Spring;
        }
    }
}