using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPowerup : PowerUp
{
    [Header("Movement")]
    [SerializeField] private float force = 100f;
    [SerializeField] private float speed = 20f;
    private new Rigidbody2D rigidbody;

    public override void Action(Peple peple)
    {   
        rigidbody = peple.GetComponent<Rigidbody2D>();
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
