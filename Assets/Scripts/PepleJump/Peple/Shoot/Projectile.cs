using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public  float speed = 2;
    public float lifetime = 2;

    [HideInInspector] public Vector2 direction = Vector2.up;

    public event System.Action<Projectile> onDespawn;

    
    private float time = 0;

    private void OnEnable()
    {
        StartCoroutine(Move());
    }

    private void OnDisable()
    {
        time = 0;
        StopAllCoroutines();
    }

    private IEnumerator Move()
    {
        while(time < lifetime)
        {
            transform.Translate(direction * speed * Time.deltaTime);

            time += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        Despawn();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<IMonster>(out var monster))
        {
            if (monster.isDead()) return;
            monster.Die();
            time = lifetime;
        }
    }

    public void Despawn()
    {
        onDespawn?.Invoke(this);
    }
}
