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
            PowerUp item = container.InstantiatePrefab(this.item).GetComponent<PowerUp>();

            item.transform.SetParent(other.transform);
            item.transform.localPosition = Vector3.zero;
            item.gameObject.SetActive(true);

            other.GetComponent<PowerupsController>().ChangePowerup(item);

            Despawn();
        }
    }

    public void Despawn()
    {
        onDespawn?.Invoke(this);
    }
}
