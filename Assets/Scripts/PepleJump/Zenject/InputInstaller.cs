using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField] Joystick joystick;
    public override void InstallBindings()
    {
        joystick.AxisOptions = AxisOptions.Horizontal;

        Container.BindInstance<Joystick>(joystick);
    }
}