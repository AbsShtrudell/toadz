using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [Zenject.Inject] private PlatformController controller;
    public bool moving = false;
    private float targetX;

    void Start()
    {
        targetX = controller.horizontalSpreadMax;
    }

    void Update()
    {
        if (moving)
        {
            if (transform.position.x == controller.horizontalSpreadMax)
                targetX = controller.horizontalSpreadMin;
            else if (transform.position.x == controller.horizontalSpreadMin)
                targetX = controller.horizontalSpreadMax;

            var nextPosition = transform.position;
            nextPosition.x = Mathf.MoveTowards(nextPosition.x, targetX, speed * Time.deltaTime);
            transform.position = nextPosition;
        }
    }
}
