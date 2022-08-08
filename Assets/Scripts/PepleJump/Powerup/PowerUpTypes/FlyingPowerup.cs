using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPowerup : PowerUp
{
    [SerializeField] private GameObject visualEffect;
    [SerializeField] private Vector2 visualEffectOffset;

    [Header("Movement")]
    [SerializeField] private float force = 100f;
    [SerializeField] private float speed = 20f;
    private new Rigidbody2D rigidbody;

    public override void Action(Peple peple)
    {   
        rigidbody = peple.GetComponent<Rigidbody2D>();

        var effect = Instantiate(visualEffect, peple.transform);
        effect.transform.localPosition = visualEffectOffset;

        void OnDespawn(PowerUp p)
        {
            Destroy(effect);
            onDespawn -= OnDespawn;
        }
        onDespawn += OnDespawn;

        base.Action(peple);
    }

    protected override void Affect()
    {
        var velocity = rigidbody.velocity;
        velocity.y = Mathf.MoveTowards(rigidbody.velocity.y, speed, Time.deltaTime * force);
        rigidbody.velocity = velocity;

        clock += Time.deltaTime;
    }
}
