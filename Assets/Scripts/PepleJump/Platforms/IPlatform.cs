using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PepleJump
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class IPlatform : MonoBehaviour
    {
        [Zenject.Inject] protected PlatformController controller;
        [Zenject.Inject] protected PlatformTraits traits;

        protected SpriteRenderer _spriteRenderer;

        public event Action<IPlatform> onDespawned;

        protected void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public abstract void Action(Peple peple);

        public abstract PlatformType GetPlatformType();
        public void Despawn()
        {
            onDespawned?.Invoke(this);
        }
    }
}