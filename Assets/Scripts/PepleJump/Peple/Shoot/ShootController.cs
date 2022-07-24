using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShootController : MonoBehaviour
{
    [Zenject.Inject] private InputHandler inputHandler;

    private ObjectPool<Projectile> projectilesPool;

    [SerializeField] private Projectile projectileRef;
    [SerializeField] private float shootDelay = 2f;
    [SerializeField] private Vector2 offset = Vector2.zero;
    [SerializeField] private float projectilesSpeed = 10f;

    private Coroutine shootCoroutine = null;
    private bool canShoot = true;

    private void Awake()
    {
        Init();
    }

    void Update()
    {
        if (!inputHandler.ActiveInput.HasFlag(InputHandler.Type.Shoot)) return;

        Shoot();
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);

        canShoot = true;
    }

    private void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            Projectile pr = projectilesPool.Get();
            pr.transform.position = transform.position + (Vector3)offset;
            pr.direction = Vector2.up;
            pr.speed = projectilesSpeed;

            StartCoroutine(ShootDelay());
        }
    }

    private void Init()
    {
        projectilesPool = new ObjectPool<Projectile>(() =>
             {
                 Projectile pr = Instantiate(projectileRef) as Projectile ;
                 pr.onDespawn += projectilesPool.Release;
                 return pr;
             }, pr =>
             {
                 pr.gameObject.SetActive(true);
             }, pr =>
             {
                 pr.gameObject.SetActive(false);
             }, pr =>
             {
                 Destroy(pr);
             }, true, 10, 20);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)offset, 0.05f);
    }
}
