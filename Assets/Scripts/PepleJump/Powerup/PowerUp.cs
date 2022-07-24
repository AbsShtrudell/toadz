using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public enum Type
    { Jetpack, Hat, Boots }

    [SerializeField] protected float _lifetime = 2f;
    [SerializeField] protected Type _type;

    public Type type => _type;

    public event System.Action<PowerUp> onDespawn;

    public abstract void Action(Peple peple);

    public void Despawn()
    {
        onDespawn?.Invoke(this);
        Destroy(gameObject);
    }
}
