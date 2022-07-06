using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(RectTransform))]
public class Slider : MonoBehaviour
{
    private enum Direction { Left = -1, Right = 1 }

    [SerializeField]
    private RectTransform ball;
    [SerializeField, Min(0f)]
    private float speed = 20f;
    [SerializeField, Min(0f)]
    private float speedFactor = 80f;
    [SerializeField]
    private float areaOffset = 88;

    private Coroutine slideCoroutine;
    private Coroutine returnCoroutine;

    private Direction direction = Direction.Right;
    private float sliderBounds;
    private float ballHalf
    { get { return ball.sizeDelta.x / 2; } }
    private float ballLocation
    { get { return ball.localPosition.x; } set { ball.localPosition = new Vector3(value, 0); } }
    private float movementBounds
    { get { return sliderBounds - ballHalf - 5; } }

    public event Action onRestored;

    void OnEnable()
    {
        sliderBounds = GetComponent<RectTransform>().sizeDelta.x / 2;
        ballLocation = -movementBounds;
        direction = Direction.Right;
    }

    public void Launch()
    {
        slideCoroutine = StartCoroutine(Slide());
    }

    private IEnumerator Slide()
    {
        while(true)
        {
            Move();
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator ReturnBall()
    {
        while (true)
        {
            float nextLocation = ballLocation - (speed * 2 * Time.deltaTime);

            if (nextLocation < -movementBounds)
            {
                nextLocation = -movementBounds;
                direction = Direction.Right;
                onRestored?.Invoke();
                yield break;
            }
            else
            {
                ballLocation = nextLocation;
                yield return new WaitForEndOfFrame();
            }
        }

    }

    private void Move()
    {
        float nextLocation = GetNextLocation();

        if (Mathf.Abs(nextLocation) > movementBounds)
        {
            nextLocation = movementBounds * (int)direction;
            direction = (Direction)((int)direction * -1);
        }

        ball.localPosition = new Vector2(nextLocation, 0);
    }

    private float GetNextLocation()
    {
        float t = ballLocation / speedFactor;
        float nextLocation = ballLocation + speed * Mathf.Exp(-0.5f * t * t) * Time.deltaTime * (int)direction;

        return nextLocation;
    }

    public void Restore()
    {
        if (slideCoroutine != null)
        {
            StopCoroutine(slideCoroutine);
            slideCoroutine = null;
        }

        returnCoroutine = StartCoroutine(ReturnBall());
    }

    public bool IsBallInArea()
    {
        return Mathf.Abs(ballLocation) < (areaOffset);
    }
}
