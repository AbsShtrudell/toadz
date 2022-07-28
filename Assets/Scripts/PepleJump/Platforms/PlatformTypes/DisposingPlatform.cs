using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class DisposingPlatform : NormalPlatform
    {
        [Zenject.Inject] private PlatformsSpawner spawner;

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public override void Action(Peple peple) 
        {
            base.Action(peple);

            StartCoroutine(Disposing(peple.jumpDelay));
        }

        IEnumerator Disposing(float timer)
        {
            yield return new WaitForSeconds(timer);

            var brokenPlatform = spawner.Spawn(PlatformType.Broken) as BrokenPlatform;
            brokenPlatform.transform.position = transform.position;
            brokenPlatform.Init(PlatformType.Disposable);

            Despawn();
        }

        public override PlatformType GetPlatformType()
        {
            return PlatformType.Disposable;
        }
    }
}