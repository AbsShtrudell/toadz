using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPowerup : PowerUp
{
    [Header("Movement")]
    [SerializeField] private float force = 100f;
    [SerializeField] private float speed = 20f;

    public override void Action(Peple peple)
    {   
        base.Action(peple);

        StartCoroutine(Move(peple.GetComponent<Rigidbody2D>()));
    }

    IEnumerator Move(Rigidbody2D rigidbody)
    {
        while (clock < _lifetime)
        {
            var velocity = rigidbody.velocity;
            velocity.y = Mathf.MoveTowards(rigidbody.velocity.y, speed, Time.deltaTime * force);
            rigidbody.velocity = velocity;

            clock += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Despawn();
    }
}
