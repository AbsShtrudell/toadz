using UnityEngine;
using Zenject;

public class PlatformTraitsInstaller : MonoInstaller
{
    [SerializeField] private PlatformTraits traits;

    public override void InstallBindings()
    {
        Container.BindInstance<PlatformTraits>(traits).AsSingle();
    }
}