using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class FragilePlatform : IPlatform
    {
        [Zenject.Inject] private PlatformsSpawner spawner;

        public override void Action(Peple peple)
        {
            var brokenPlatform = spawner.Spawn(PlatformType.Broken) as BrokenPlatform;
            brokenPlatform.transform.position = transform.position;
            brokenPlatform.Init(PlatformType.Fragile);
            peple.velocity = peple.velocity;

            Despawn();
        }

        public override PlatformType GetPlatformType()
        {
            return PlatformType.Fragile;
        }
    }
}