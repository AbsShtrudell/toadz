using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private GameObject platformRef;
    [SerializeField, Min(0f)] private float startVerticalSpreadMin = 0.5f;
    [SerializeField, Min(0f)] private float startVerticalSpreadMax = 1f;
    [SerializeField, Min(0f)] private float endVerticalSpreadMin = 4f;
    [SerializeField, Min(0f)] private float endVerticalSpreadMax = 4f;
    [SerializeField, Min(0f)] private float verticalSpreadMinDelta = 0.1f;
    [SerializeField, Min(0f)] private float verticalSpreadMaxDelta = 0.1f;
    [SerializeField] private float _horizontalSpreadMin = -1.8f;
    [SerializeField] private float _horizontalSpreadMax = 1.8f;
    [SerializeField, Min(1)] protected int maxPlatformCount = 7;
    [Zenject.Inject] private Zenject.DiContainer container;
    private float currentVerticalSpreadMin;
    private float currentVerticalSpreadMax;
    private float nextY;

    public float horizontalSpreadMin => _horizontalSpreadMin;
    public float horizontalSpreadMax => _horizontalSpreadMax;

    protected virtual void Start()
    {
        currentVerticalSpreadMin = startVerticalSpreadMin;
        currentVerticalSpreadMax = startVerticalSpreadMax;
        nextY = platformRef.transform.position.y + Random.Range(currentVerticalSpreadMin, currentVerticalSpreadMax);

        for (int i = 1; i <= maxPlatformCount; i++)
        {
            var p = container.InstantiatePrefab(platformRef).GetComponent<Platform>();

            SpawnNext(p);
        }
    }

    public virtual void SpawnNext(Platform p)
    {
        Vector3 position = p.transform.position;
        position.y = nextY;
        position.x = Random.Range(horizontalSpreadMin, horizontalSpreadMax);
        p.transform.position = position;

        p.moving = Random.Range(0, 5) == 0;

        nextY += Random.Range(currentVerticalSpreadMin, currentVerticalSpreadMax);

        currentVerticalSpreadMin = Mathf.MoveTowards(currentVerticalSpreadMin, endVerticalSpreadMin, verticalSpreadMinDelta);
        currentVerticalSpreadMax = Mathf.MoveTowards(currentVerticalSpreadMax, endVerticalSpreadMax, verticalSpreadMaxDelta);
    }
}
