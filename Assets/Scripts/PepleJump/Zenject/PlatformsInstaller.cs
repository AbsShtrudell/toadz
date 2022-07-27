using UnityEngine;
using Zenject;
using PepleJump;
using UnityEngine.Pool;

public class PlatformsInstaller : MonoInstaller
{
    [Header("Controllers")]
    [SerializeField] PlatformController platformController;
    [SerializeField] PlatformsSpawner platformsSpawner;
    [SerializeField] PlatformTraits platformTraits;
    [Header("Platform Refs")]
    [SerializeField] NormalPlatform normalPlatform;
    [SerializeField] SpringPlatform springPlatform;
    [SerializeField] FragilePlatform fragilePlatform;
    [SerializeField] BrokenPlatform brokenPlatform;
    [SerializeField] DisposingPlatform disposingPlatform;
    [SerializeField] MovingHorizontallyPlatform movingHorizontallyPlatform;
    [SerializeField] VoidHole voidHole;
    [SerializeField] ExplosivePlatform explosivePlatform;
    [SerializeField] SittingMonster sittingMonster;
    [SerializeField] FlyingMonster flyingMonster;

    public override void InstallBindings()
    {
        Container.Bind<ObjectPool<NormalPlatform>>()
            .FromInstance(InitPool<NormalPlatform>(normalPlatform))
            .AsSingle();

        Container.Bind<ObjectPool<SpringPlatform>>()
            .FromInstance(InitPool<SpringPlatform>(springPlatform))
            .AsSingle();

        Container.Bind<ObjectPool<FragilePlatform>>()
            .FromInstance(InitPool<FragilePlatform>(fragilePlatform))
            .AsSingle();

        Container.Bind<ObjectPool<BrokenPlatform>>()
            .FromInstance(InitPool<BrokenPlatform>(brokenPlatform))
            .AsSingle();

        Container.Bind<ObjectPool<DisposingPlatform>>()
            .FromInstance(InitPool<DisposingPlatform>(disposingPlatform))
            .AsSingle();
            
        Container.Bind<ObjectPool<MovingHorizontallyPlatform>>()
            .FromInstance(InitPool<MovingHorizontallyPlatform>(movingHorizontallyPlatform))
            .AsSingle();

        Container.Bind<ObjectPool<VoidHole>>()
            .FromInstance(InitPool<VoidHole>(voidHole))
            .AsSingle();

        Container.Bind<ObjectPool<ExplosivePlatform>>()
            .FromInstance(InitPool<ExplosivePlatform>(explosivePlatform))
            .AsSingle();

        Container.Bind<ObjectPool<SittingMonster>>()
            .FromInstance(InitPool<SittingMonster>(sittingMonster))
            .AsSingle();

        Container.Bind<ObjectPool<FlyingMonster>>()
            .FromInstance(InitPool<FlyingMonster>(flyingMonster))
            .AsSingle();

        Container.Bind<PlatformsSpawner>().FromInstance(platformsSpawner).AsSingle();
        Container.Bind<PlatformController>().FromInstance(platformController).AsSingle();
        Container.Bind<PlatformTraits>().FromInstance(platformTraits).AsSingle();
    }

    private ObjectPool<T> InitPool<T>(T platform) where T : IPlatform
    {
        ObjectPool<T> pool = new ObjectPool<T>(() =>
        {
            var go = Container.InstantiatePrefab(platform);
            var p = go.GetComponent<T>();

            go.SetActive(false);
            go.transform.localPosition = new Vector3(10, -10, 0);

            return p;
        }, pl =>
        {
            pl.gameObject.SetActive(true);
        }, pl =>
        {
            pl.gameObject.SetActive(false);
        }, pl =>
        {
            Destroy(pl);
        }, true, 5, 10);

        return pool;
    }
}