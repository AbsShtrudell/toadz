using UnityEngine;
using Zenject;

public class DJCloudsInstaller : MonoInstaller
{
    [SerializeField] private CloudController controller;

    public override void InstallBindings()
    {
        Container.BindInstance<CloudController>(controller).AsSingle();
    }
}