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

        [SerializeField] protected Vector2 spawnFree;
        [SerializeField] protected Vector2 offset;

        protected SpriteRenderer _spriteRenderer;

        public Vector2 SpawnFree => spawnFree;
        public Vector2 Offset => offset;

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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + (Vector3)offset, SpawnFree);
        }
    }
}