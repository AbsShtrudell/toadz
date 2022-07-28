using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class BrokenPlatform : IPlatform
    {
        private Coroutine falling;

        private void OnEnable()
        {
            StartCoroutine(Falling());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public void Init(PlatformType type)
        {
            switch(type)
            {
                case PlatformType.Disposable:
                    GetComponent<SpriteRenderer>().color = Color.white;
                    break;
                case PlatformType.Fragile:
                    GetComponent<SpriteRenderer>().color = new Color(125f/ 255f,72f / 255f, 32f / 255f);
                    break;

            }
        }

        public override void Action(Peple peple)
        {
        }

        IEnumerator Falling()
        {
            while (true)
            {
                transform.Translate(Vector3.down * traits.fallingSpeed * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
        }

        public override PlatformType GetPlatformType()
        {
           return PlatformType.Broken;
        }
    }
}