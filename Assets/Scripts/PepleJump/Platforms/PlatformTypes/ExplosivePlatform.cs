using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class ExplosivePlatform : NormalPlatform
    {
        [Zenject.Inject] private ScoreController scoreController;
        private bool active = false;

        void OnEnable()
        {
            active = false;
            _spriteRenderer.color = traits.explosiveInactiveColor;
        }

        public override void Action(Peple peple)
        {
            if (!active)
            {
                base.Action(peple);
            
                active = true;
                _spriteRenderer.color = traits.explosiveActiveColor;
            }
            else
            {
                peple.JumpImmediately(traits.jumpForceSpring);

                active = false;
                _spriteRenderer.color = traits.explosiveInactiveColor;

                scoreController.AddScore(-traits.explosiveScoreLoss);
                Despawn();
            }
        }

        public override PlatformType GetPlatformType()
        {
            return PlatformType.Explosive;
        }
    }
}
