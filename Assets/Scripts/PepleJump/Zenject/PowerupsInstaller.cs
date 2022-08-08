using UnityEngine;
using Zenject;
using UnityEngine.Pool;

namespace PepleJump
{
    public class PowerupsInstaller : MonoInstaller
    {
        [SerializeField] private PowerupTraits traits;
        [SerializeField] private PowerupsController controller;

        [Header("Controllers")]
        [SerializeField] private PowerupSpawner spawner;

        [Header("Pickup Refs")]
        [SerializeField] private ItemPickup pickupJetpack;
        [SerializeField] private ItemPickup pickupHat;
        [SerializeField] private ItemPickup pickupBoots;
        [SerializeField] private ItemPickup pickupShield;

        public override void InstallBindings()
        {
            Container.BindInstance<ObjectPool<ItemPickup>>(InitPool(pickupJetpack)).WithId(PowerUp.Type.Jetpack);
            Container.BindInstance<ObjectPool<ItemPickup>>(InitPool(pickupHat)).WithId(PowerUp.Type.Hat);
            Container.BindInstance<ObjectPool<ItemPickup>>(InitPool(pickupBoots)).WithId(PowerUp.Type.Boots);
            Container.BindInstance<ObjectPool<ItemPickup>>(InitPool(pickupShield)).WithId(PowerUp.Type.Shield);

            Container.BindInstance<PowerupSpawner>(spawner);
            Container.BindInstance<PowerupTraits>(traits);
            Container.BindInstance<PowerupsController>(controller);
        }

        private ObjectPool<ItemPickup> InitPool(ItemPickup itemPickup)
        {
            ObjectPool<ItemPickup> pool = null;

            pool = new ObjectPool<ItemPickup>(() =>
            {
                ItemPickup item = Container.InstantiatePrefab(itemPickup).GetComponent<ItemPickup>();
                item.onDespawn += pool.Release;
                return item;
            }, pl =>
            {
                pl.gameObject.SetActive(true);
            }, pl =>
            {
                pl.gameObject.SetActive(false);
            }, pl =>
            {
                Destroy(pl);
            }, true, 5, 10);

            return pool;
        }
    }
}
