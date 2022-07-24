using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;



public class PowerupSpawner : MonoBehaviour
{
    [Zenject.Inject(Id = "JetpackPool")] ObjectPool<ItemPickup> jetpackPool;

    public ItemPickup Spawn(PowerUp.Type type)
    {
        ItemPickup item = null;

        switch (type)
        {
            case PowerUp.Type.Jetpack:
                item = jetpackPool.Get();
                break;
            case PowerUp.Type.Boots:
                break;
            case PowerUp.Type.Hat:
                break;
        }

        return item;
    }

    public int InGame(PowerUp.Type type)
    {
        switch (type)
        {
            case PowerUp.Type.Jetpack:
                return jetpackPool.CountActive;
            case PowerUp.Type.Boots:
                return 0;
            case PowerUp.Type.Hat:
                return 0;
            default:
                return 0;
        }
    }
}
