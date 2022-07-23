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

            var brokenPlatform = spawner.Spawn(PlatformType.Broken);
            brokenPlatform.transform.position = transform.position;

            Despawn();
        }

        public override PlatformType GetPlatformType()
        {
            return PlatformType.Disposable;
        }
    }
}