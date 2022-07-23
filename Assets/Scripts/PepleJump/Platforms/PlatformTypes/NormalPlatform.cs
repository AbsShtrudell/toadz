using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class NormalPlatform : IPlatform
    {
        public override void Action(Peple peple)
        {
            peple.Jump(traits.jumpForceNormal, transform);
        }

        public override PlatformType GetPlatformType()
        {
            return PlatformType.Normal;
        }
    }
}
