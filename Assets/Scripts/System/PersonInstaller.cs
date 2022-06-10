using UnityEngine;
using Zenject;

public class PersonInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject personPref;
    public override void InstallBindings()
    {
        PersonController personController = personPref.GetComponent<PersonController>();
        Container.Bind<PersonController>().FromInstance(personController).AsSingle();
    }
}