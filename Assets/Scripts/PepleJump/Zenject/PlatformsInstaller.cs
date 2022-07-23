using UnityEngine;
using Zenject;
using PepleJump;
using UnityEngine.Pool;

public class PlatformsInstaller : MonoInstaller
{
    [SerializeField] PlatformController platformController;
    [SerializeField] PlatformsSpawner platformsSpawner;
    [SerializeField] PlatformTraits platformTraits;
    [SerializeField] NormalPlatform normalPlatform;
    [SerializeField] SpringPlatform springPlatform;
    [SerializeField] FragilePlatform fragilePlatform;
    [SerializeField] BrokenPlatform brokenPlatform;
    [SerializeField] DisposingPlatform disposingPlatform;

    public override void InstallBindings()
    {
        Container.Bind<ObjectPool<NormalPlatform>>().FromInstance(InitPool<NormalPlatform>(normalPlatform)).AsSingle();
        Container.Bind<ObjectPool<SpringPlatform>>().FromInstance(InitPool<SpringPlatform>(springPlatform)).AsSingle();
        Container.Bind<ObjectPool<FragilePlatform>>().FromInstance(InitPool<FragilePlatform>(fragilePlatform)).AsSingle();
        Container.Bind<ObjectPool<BrokenPlatform>>().FromInstance(InitPool<BrokenPlatform>(brokenPlatform)).AsSingle();
        Container.Bind<ObjectPool<DisposingPlatform>>().FromInstance(InitPool<DisposingPlatform>(disposingPlatform)).AsSingle();

        Container.Bind<PlatformsSpawner>().FromInstance(platformsSpawner);
        Container.Bind<PlatformController>().FromInstance(platformController);
        Container.Bind<PlatformTraits>().FromInstance(platformTraits);
    }

    private ObjectPool<T> InitPool<T>(T platform) where T : IPlatform
    {
        ObjectPool<T> pool = new ObjectPool<T>(() =>
        {
            var go = Container.InstantiatePrefab(platform);
            go.SetActive(false);
            go.transform.localPosition = new Vector3(10, -10, 0);
            return go.GetComponent<T>();
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