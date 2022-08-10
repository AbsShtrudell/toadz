using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class MovingHorizontallyPlatform : NormalPlatform
    {
        protected float targetX;
        protected float positionA;
        protected float positionB;

        void Start()
        {
            positionA = controller.horizontalSpreadMin;
            positionB = controller.horizontalSpreadMax;
            
            targetX = controller.horizontalSpreadMin;
        }

        protected virtual void Update()
        {
            Move();
        }

        protected void Move()
        {
            if (transform.position.x == positionA)
                targetX = positionB;
            else if (transform.position.x == positionB)
                targetX = positionA;

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