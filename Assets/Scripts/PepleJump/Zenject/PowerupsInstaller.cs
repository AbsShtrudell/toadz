using UnityEngine;
using Zenject;
using UnityEngine.Pool;

namespace PepleJump
{
    public class PowerupsInstaller : MonoInstaller
    {
        [SerializeField] private PowerupTraits traits;
        [Header("Controllers")]
        [SerializeField] private PowerupSpawner spawner;
        [Header("Pickup Refs")]
        [SerializeField] private ItemPickup pickupJetpack;

        public override void InstallBindings()
        {
            Container.BindInstance<ObjectPool<ItemPickup>>(InitPool(pickupJetpack)).WithId("JetpackPool");

            Container.BindInstance<PowerupSpawner>(spawner);
            Container.BindInstance<PowerupTraits>(traits);
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
