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
        if (maxPlatformCount >= numberOfTargetPlatform)
            maxPlatformCount = numberOfTargetPlatform - 1;

        currentPlatformNumber = maxPlatformCount;
        base.Start();
    }

    public override void SpawnNext(Platform p)
    {
        if (currentPlatformNumber < numberOfTargetPlatform)
        {
            base.SpawnNext(p);
            currentPlatformNumber++;
        }
        else if (currentPlatformNumber == numberOfTargetPlatform)
        {
            p.type = Platform.Type.Target;
            MovePlatform(p);
            currentPlatformNumber++;
        }
    }

}
