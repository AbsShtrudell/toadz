using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class MovingHorizontallyPlatform : NormalPlatform
    {
        private float targetX;

        void Start()
        {
            targetX = controller.horizontalSpreadMin;
        }

        void Update()
        {
            if (transform.position.x == controller.horizontalSpreadMax)
                targetX = controller.horizontalSpreadMin;
            else if (transform.position.x == controller.horizontalSpreadMin)
                targetX = controller.horizontalSpreadMax;

            var nextPosition = transform.position;
            nextPosition.x = Mathf.MoveTowards(nextPosition.x, targetX, traits.horizontalSpeed * Time.deltaTime);
            transform.position = nextPosition;
        }

        public override PlatformType GetPlatformType()
        {
            return PlatformType.MovingHorizontally;
        }
    }
}