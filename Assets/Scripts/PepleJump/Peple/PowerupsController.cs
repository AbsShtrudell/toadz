using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsController : MonoBehaviour
{
    [Zenject.Inject] Peple peple;

    private PowerUp _activePowerup = null;

    public PowerUp ActivePowerup => _activePowerup;

    public void ChangePowerup(PowerUp powerup)
    {
        _activePowerup = powerup;
        _activePowerup.onDespawn += DeattachPowerup;
        _activePowerup.Action(peple);
    }

    private void DeattachPowerup(PowerUp powerup)
    {
        _activePowerup = null;
    }
}
