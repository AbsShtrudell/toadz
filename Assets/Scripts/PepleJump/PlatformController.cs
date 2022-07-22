using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [System.Serializable]
    private class Chance
    {
        public delegate void MakeAction(Platform p);

        public Platform.Type type;
        public float value;
        public MakeAction action;
    }

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
    [SerializeField] private Transform jetpackPool;
    [SerializeField] private Chance[] chances;
    [Zenject.Inject] private Zenject.DiContainer container;
    [Zenject.Inject] private PlatformTraits traits;
    private float currentVerticalSpreadMin;
    private float currentVerticalSpreadMax;
    private float nextY;
    private float nextX;
    private float sumChance = 0f;
    private bool aboveMax = false;
    private Platform lastNormal;

    public float horizontalSpreadMin => _horizontalSpreadMin;
    public float horizontalSpreadMax => _horizontalSpreadMax;

    public event System.Action onPepleWon;

    protected virtual void Start()
    {
        foreach (var c in chances)
        {
            sumChance += c.value;

            switch (c.type)
            {
                case Platform.Type.Normal:
                    c.action = MakeNormal;
                    break;
                case Platform.Type.Spring:
                    c.action = MakeSpring;
                    break;
                case Platform.Type.Disposable:
                    c.action = MakeDisposable;
                    break;
                case Platform.Type.Fragile:
                    c.action = MakeFragile;
                    break;
                default:
                    Debug.LogError("Missing action for " + c.type);
                    break;
            }
        }

        currentVerticalSpreadMin = startVerticalSpreadMin;
        currentVerticalSpreadMax = startVerticalSpreadMax;
        nextY = platformRef.transform.position.y + Random.Range(currentVerticalSpreadMin, currentVerticalSpreadMax);
        nextX = Random.Range(horizontalSpreadMin, horizontalSpreadMax);
        lastNormal = platformRef.GetComponent<Platform>();

        for (int i = 1; i <= maxPlatformCount; i++)
        {
            var p = container.InstantiatePrefab(platformRef).GetComponent<Platform>();
            p.onPepleWon += Won;
            SpawnNext(p);
        }
    }

    private void Won()
    {
        onPepleWon?.Invoke();
    }

    public virtual void SpawnNext(Platform p)
    {
        p.StopHorizontalMovement();

        aboveMax = nextY - lastNormal.transform.position.y > endVerticalSpreadMax;

        if (aboveMax)
        {
            nextY = lastNormal.transform.position.y + endVerticalSpreadMax;
        }

        float randomValue = Random.Range(0f, sumChance);

        float lowerBound = 0f, upperBound = 0f;
        foreach (var c in chances)
        {
            upperBound += c.value;

            if (randomValue >= lowerBound && randomValue <= upperBound)
            {
                c.action(p);
                break;
            }

            lowerBound += c.value;
        }

        MovePlatform(p);
    }

    private void MakeDisposable(Platform p)
    {
        p.type = Platform.Type.Disposable;

        lastNormal = p;
    }

    private void MakeFragile(Platform p)
    {
        if (aboveMax)
        {
            MakeNormal(p);
        }
        else
            p.type = Platform.Type.Fragile;
    }

    private void MakeSpring(Platform p)
    {
        p.type = Platform.Type.Spring;

        lastNormal = p;
    }

    private void MakeNormal(Platform p)
    {
        p.type = Platform.Type.Normal;

        lastNormal = p;

        if (Random.Range(0, 10) < 4)
        {
            p.StartHorizontalMovement();
        }
        else if (jetpackPool.childCount > 0 && Random.Range(0, 10) == 0)
        {
            var item = jetpackPool.GetChild(0);

            item.SetParent(null);
            item.position = new Vector2(nextX, nextY + 0.5f);
            item.gameObject.SetActive(true);
        }
    }

    protected void MovePlatform(Platform p)
    {
        Vector3 position = p.transform.position;
        position.y = nextY;
        position.x = nextX;
        p.transform.position = position;

        nextY += Random.Range(currentVerticalSpreadMin, currentVerticalSpreadMax);
        nextX = Random.Range(horizontalSpreadMin, horizontalSpreadMax);

        currentVerticalSpreadMin = Mathf.MoveTowards(currentVerticalSpreadMin, endVerticalSpreadMin, verticalSpreadMinDelta);
        currentVerticalSpreadMax = Mathf.MoveTowards(currentVerticalSpreadMax, endVerticalSpreadMax, verticalSpreadMaxDelta);
    }
}
