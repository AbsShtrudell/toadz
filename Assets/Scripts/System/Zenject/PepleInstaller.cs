using UnityEngine;
using Zenject;

public class PepleInstaller : MonoInstaller
{
    [SerializeField] private Peple peple;

    public override void InstallBindings()
    {
        Container.BindInstance<Peple>(peple).AsSingle();
    }
}