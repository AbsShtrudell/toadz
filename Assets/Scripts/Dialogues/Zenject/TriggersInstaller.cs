using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Dialogues;
using Dialogues.UI;

public class TriggersInstaller : MonoInstaller
{
    [SerializeField]
    private UIController dUIController;
    [SerializeField]
    private GMDialogue gmDialogue;
    [SerializeField]
    private DialogueController dialogueController;

    public override void InstallBindings()
    {
        TRClear trClear = new TRClear(dUIController);
        TRFly trFly = new TRFly(gmDialogue);
        TRDoodle trDoodle = new TRDoodle(gmDialogue);
        TRJoust trJoust = new TRJoust(gmDialogue);
        TREnd trEnd = new TREnd(gmDialogue, dialogueController);

        TriggersController triggersController = new TriggersController(new List<ITrigger> { trClear, trFly, trJoust, trDoodle, trEnd});
        Container.BindInstance<TriggersController>(triggersController).AsSingle();
    }
}