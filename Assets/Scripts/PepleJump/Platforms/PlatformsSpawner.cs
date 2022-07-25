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
        [Zenject.Inject] private ObjectPool<MovingHorizontallyPlatform> _horizontalPlatformPool;
        [Zenject.Inject] private ObjectPool<VoidHole> _voidHolePool;
        [Zenject.Inject] private ObjectPool<ExplosivePlatform> _explosivePlatformPool;

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
                case PlatformType.MovingHorizontally:
                    platform = _horizontalPlatformPool.Get();
                    break;
                case PlatformType.VoidHole:
                    platform = _voidHolePool.Get();
                    break;
                case PlatformType.Explosive:
                    platform = _explosivePlatformPool.Get();
                    break;
                case PlatformType.Target:
                    return null;
            }
            platform.onDespawned += Despawn;

            return platform;
        }

        public int InGame(PlatformType type)
        {
            switch (type)
            {
                case PlatformType.Normal:
                    return _normalPlatformPool.CountActive;
                case PlatformType.Spring:
                    return _springPlatformPool.CountActive;
                case PlatformType.Fragile:
                    return _fragilePlatformPool.CountActive;
                case PlatformType.Disposable:
                    return _disposingPlatformPool.CountActive;
                case PlatformType.MovingHorizontally:
                    return _horizontalPlatformPool.CountActive;
                case PlatformType.VoidHole:
                    return _voidHolePool.CountActive;
                case PlatformType.Explosive:
                    return _explosivePlatformPool.CountActive;
                default:
                    return 0;
            }
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
                case PlatformType.MovingHorizontally:
                    _horizontalPlatformPool.Release((MovingHorizontallyPlatform)platform);
                    break;
                case PlatformType.VoidHole:
                    _voidHolePool.Release((VoidHole)platform);
                    break;
                case PlatformType.Explosive:
                    _explosivePlatformPool.Release((ExplosivePlatform)platform);
                    break;
                case PlatformType.Target:
                    break;
            }
        }
    }
}
