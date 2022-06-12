using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private GameObject platformRef;
    [SerializeField] private float verticalSpreadMin = 2f;
    [SerializeField] private float verticalSpreadMax = 4f;
    [SerializeField] private float horizontalSpreadMin = -1.8f;
    [SerializeField] private float horizontalSpreadMax = 1.8f;
    [SerializeField, Min(1)] protected int maxPlatformCount = 7;
    private float nextY;

    protected virtual void Start()
    {
        nextY = platformRef.transform.position.y + Random.Range(verticalSpreadMin, verticalSpreadMax);

        for (int i = 1; i <= 7; i++)
        {
            var t = Instantiate(platformRef).transform;

            SpawnNext(t);
        }
    }

    public virtual void SpawnNext(Transform t)
    {
        Vector3 position = Vector3.zero;
        position.y = nextY;
        position.x = Random.Range(horizontalSpreadMin, horizontalSpreadMax);

        t.position = position;

        nextY += Random.Range(verticalSpreadMin, verticalSpreadMax);

        verticalSpreadMin = Mathf.MoveTowards(verticalSpreadMin, verticalSpreadMax, 0.05f);
    }
}
