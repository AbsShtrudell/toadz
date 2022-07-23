using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace PepleJump
{
    public class PlatformsSpawner : MonoBehaviour
    {
        [Zenject.Inject] private ObjectPool<NormalPlatform> _normalPlatformPool;
        [Zenject.Inject] private ObjectPool<SpringPlatform> _springPlatformPool;
        [Zenject.Inject] private ObjectPool<FragilePlatform> _fragilePlatformPool;
        [Zenject.Inject] private ObjectPool<BrokenPlatform> _brokenPlatformPool;
        [Zenject.Inject] private ObjectPool<DisposingPlatform> _disposingPlatformPool;
        public IPlatform Spawn(PlatformType type)
        {
            IPlatform platform = null;

            switch(type)
            {
                case PlatformType.Normal:
                    platform = _normalPlatformPool.Get();
                    break;
                case PlatformType.Spring:
                    platform = _springPlatformPool.Get();
                    break;
                case PlatformType.Fragile:
                    platform = _fragilePlatformPool.Get();
                    break;
                case PlatformType.Disposable:
                    platform = _disposingPlatformPool.Get();
                    break;
                case PlatformType.Broken:
                    platform = _brokenPlatformPool.Get();
                    break;
                case PlatformType.Target:
                    return null;
            }
            platform.onDespawned += Despawn;

            return platform;
        }

        public int InGame(PlatformType type)
        {
            int result = 0;

            switch (type)
            {
                case PlatformType.Normal:
                    result = _normalPlatformPool.CountActive;
                    break;
                case PlatformType.Spring:
                    result = _springPlatformPool.CountActive;
                    break;
                case PlatformType.Fragile:
                    result = _fragilePlatformPool.CountActive;
                    break;
                case PlatformType.Disposable:
                    result = _disposingPlatformPool.CountActive;
                    break;
                case PlatformType.Target:
                    break;
            }
            return result;
        }

        private void Despawn(IPlatform platform)
        {
            platform.onDespawned -= Despawn;

            switch (platform.GetPlatformType())
            {
                case PlatformType.Normal:
                    _normalPlatformPool.Release((NormalPlatform)platform);
                    break;
                case PlatformType.Spring:
                    _springPlatformPool.Release((SpringPlatform)platform);
                    break;
                case PlatformType.Fragile:
                    _fragilePlatformPool.Release((FragilePlatform)platform);
                    break;
                case PlatformType.Disposable:
                    _disposingPlatformPool.Release((DisposingPlatform)platform);
                    break;
                case PlatformType.Broken:
                    _brokenPlatformPool.Release((BrokenPlatform)platform);
                    break;
                case PlatformType.Target:
                    break;
            }
        }
    }
}
