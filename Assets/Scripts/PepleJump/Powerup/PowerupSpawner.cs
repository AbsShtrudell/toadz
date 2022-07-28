using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;



public class PowerupSpawner : MonoBehaviour
{
    [Zenject.Inject(Id = PowerUp.Type.Jetpack)] ObjectPool<ItemPickup> jetpackPool;
    [Zenject.Inject(Id = PowerUp.Type.Hat)] ObjectPool<ItemPickup> hatPool;
    [Zenject.Inject(Id = PowerUp.Type.Boots)] ObjectPool<ItemPickup> bootsPool;
    [Zenject.Inject(Id = PowerUp.Type.Shield)] ObjectPool<ItemPickup> shieldPool;

    public ItemPickup Spawn(PowerUp.Type type)
    {
        switch (type)
        {
            case PowerUp.Type.Jetpack:
                return jetpackPool.Get();
            case PowerUp.Type.Boots:
                return bootsPool.Get();
            case PowerUp.Type.Hat:
                return hatPool.Get();
            case PowerUp.Type.Shield:
                return shieldPool.Get();
            default:
                return null;
        }
    }

    public int InGame()
    {
        return jetpackPool.CountActive + hatPool.CountActive + bootsPool.CountActive + shieldPool.CountActive;
    }
}
