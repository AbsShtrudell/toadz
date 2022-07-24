using UnityEngine;
using Zenject;

public class PepleInstaller : MonoInstaller
{
    [SerializeField] private Peple peple;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private ShootController shootController;
    public override void InstallBindings()
    {
        Container.BindInstance<Peple>(peple).AsSingle();
        Container.BindInstance<InputHandler>(inputHandler).AsSingle();
        Container.BindInstance<ShootController>(shootController).AsSingle();
    }
}