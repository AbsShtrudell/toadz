using UnityEngine;
using Zenject;

public class DialogueInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject dialogueControllerPref;
    public override void InstallBindings()
    {
        DialogueController dialogueController = dialogueControllerPref.GetComponent<DialogueController>();
        Container.Bind<DialogueController>().FromInstance(dialogueController).AsSingle();
    }
}