using UnityEngine;
using Zenject;

public class FliesInstaller : MonoInstaller
{
    [SerializeField] private FliesController controller;

    public override void InstallBindings()
    {
        Container.BindInstance<FliesController>(controller).AsSingle();
    }
}