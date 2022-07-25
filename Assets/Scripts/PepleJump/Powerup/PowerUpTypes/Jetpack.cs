using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : PowerUp
{
    [Header("Movement")]
    [SerializeField] private float time = 3f;
    [SerializeField] private float force = 100f;
    [SerializeField] private float speed = 20f;

    private new Rigidbody2D rigidbody;

    public override void Action(Peple peple)
    {   
        peple.gameObject.layer = LayerMask.NameToLayer("Invincible");

        void OnDespawn(PowerUp p)
        {
            peple.gameObject.layer = LayerMask.NameToLayer("Peple");
            onDespawn -= OnDespawn;
        }
        onDespawn += OnDespawn;

        StartCoroutine(Move());
    }

    [Zenject.Inject]
    void SetRigidbody(Peple peple)
    {
        rigidbody = peple.GetComponent<Rigidbody2D>();
    }

    IEnumerator Move()
    {
        float time = 0;
        while( time < _lifetime)
        {
            rigidbody.velocity = Vector2.MoveTowards(rigidbody.velocity, Vector2.up * speed, Time.deltaTime * force);

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Despawn();
    }
}
