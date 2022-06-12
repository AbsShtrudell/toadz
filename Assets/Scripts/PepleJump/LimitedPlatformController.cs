using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedPlatformController : PlatformController
{
    [SerializeField] private GameObject targetRef;
    [SerializeField, Min(1f)] private int numberOfTargetPlatform = 20;
    private int currentPlatformNumber;

    protected override void Start()
    {
        currentPlatformNumber = maxPlatformCount;
        base.Start();
    }

    public override void SpawnNext(Transform t)
    {
        if (currentPlatformNumber <= numberOfTargetPlatform)
        {
            base.SpawnNext(t);

            if (currentPlatformNumber == numberOfTargetPlatform)
            {
                Instantiate(targetRef, t).transform.localPosition = new Vector3(0f, 1f, 0f);
            }

            currentPlatformNumber++;
        }
    }

}
