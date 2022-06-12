using UnityEngine;
using Zenject;

public class GMInstaller : MonoInstaller
{
    [SerializeField]
    private GMDialogue dialogue;
    [SerializeField]
    private GMDoodlejump doodlejump;
    [SerializeField]
    private GMFly fly;
    [SerializeField]
    private GMJoust joust;
    [SerializeField]
    private GamemodeController gamemodeController;
    [SerializeField]
    private TransitionHandler transitionHandler;
    public override void InstallBindings()
    {
        Container.Bind<GMDialogue>().FromInstance(dialogue).AsSingle();
        Container.Bind<GMDoodlejump>().FromInstance(doodlejump).AsSingle();
        Container.Bind<GMFly>().FromInstance(fly).AsSingle();
        Container.Bind<GMJoust>().FromInstance(joust).AsSingle();
        Container.Bind<TransitionHandler>().FromInstance(transitionHandler).AsSingle();
        Container.Bind<GamemodeController>().FromInstance(gamemodeController).AsSingle();
    }
}