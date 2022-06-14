using UnityEngine;
using Zenject;

public class PlatformInstaller : MonoInstaller
{
    [SerializeField] private PlatformController controller;

    public override void InstallBindings()
    {
        Container.BindInstance<PlatformController>(controller).AsSingle();
    }
}