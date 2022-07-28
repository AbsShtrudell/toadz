using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class ItemPickup : MonoBehaviour
{
    [Zenject.Inject] Zenject.DiContainer container;
    [SerializeField] private PowerUp item;

    public event System.Action<ItemPickup> onDespawn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Peple>() != null)        
        {
            var controller = other.GetComponent<PowerupsController>();

            if (controller.ActivePowerup?.type == item.type)
            {
                controller.ActivePowerup.Reset();
            }
            else
            {
                PowerUp powerUpInstance = container.InstantiatePrefab(item).GetComponent<PowerUp>();
                controller.ChangePowerup(powerUpInstance);            
            }
            
            Despawn();
        }
    }

    public void Despawn()
    {
        onDespawn?.Invoke(this);
    }
}
