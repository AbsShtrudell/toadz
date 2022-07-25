using UnityEngine;
using Zenject;

public class PepleInstaller : MonoInstaller
{
    [SerializeField] private Peple peple;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private ShootController shootController;
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private PepleJumpController pepleJumpController;

    public override void InstallBindings()
    {
        Container.BindInstance<Peple>(peple).AsSingle();
        Container.BindInstance<InputHandler>(inputHandler).AsSingle();
        Container.BindInstance<ShootController>(shootController).AsSingle();
        Container.BindInstance<ScoreController>(scoreController).AsSingle();
        Container.BindInstance<PepleJumpController>(pepleJumpController).AsSingle();
    }
}