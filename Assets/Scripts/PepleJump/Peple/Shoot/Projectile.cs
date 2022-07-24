using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public  float speed = 2;
    public float lifetime = 2;

    [HideInInspector] public Vector2 direction = Vector2.up;

    public event System.Action<Projectile> onDespawn;

    private void OnEnable()
    {
        StartCoroutine(Move());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Move()
    {
        float time = 0;
        while(time < lifetime)
        {
            transform.Translate(direction * speed * Time.deltaTime);

            time += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        Despawn();
    }

    public void Despawn()
    {
        onDespawn?.Invoke(this);
    }
}
