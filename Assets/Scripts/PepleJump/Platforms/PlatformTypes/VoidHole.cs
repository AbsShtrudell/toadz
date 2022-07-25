using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class VoidHole : IPlatform
    {
        public override void Action(Peple peple)
        {

        }

        public override PlatformType GetPlatformType()
        {
            return PlatformType.VoidHole;
        }
    }
}
