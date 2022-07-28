using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type
    { Jetpack, Hat, Boots, Shield }

    [SerializeField] protected float _lifetime = 2f;
    [SerializeField] protected Type _type;
    [SerializeField] protected string triggerName;
    protected float clock = 0f;

    public Type type => _type;

    public event System.Action<PowerUp> onDespawn;

    public virtual void Action(Peple peple)
    {
        peple.gameObject.layer = LayerMask.NameToLayer("Invincible");
        
        var animator = peple.GetComponent<Animator>();
        animator.SetTrigger(triggerName);

        void OnDespawn(PowerUp p)
        {
            peple.gameObject.layer = LayerMask.NameToLayer("Peple");
            animator.SetTrigger("ItemLoss");
            onDespawn -= OnDespawn;
        }
        onDespawn += OnDespawn;
    }

    private void Update()
    {
        if (clock <= _lifetime)
        {
            Affect();

            clock += Time.deltaTime;
        }
        else
        {
            Despawn();
        }
    }

    protected virtual void Affect()
    {
        return;
    }

    public void Despawn()
    {
        onDespawn?.Invoke(this);
        Destroy(gameObject);
    }

    public void Reset()
    {
        clock = 0f;
    }
}
