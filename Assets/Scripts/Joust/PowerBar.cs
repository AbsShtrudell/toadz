using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    [System.Serializable]
    public enum Direction : int
    {
        Down = -1, Up = 1 
    }

    [SerializeField] private Direction initialDirection = Direction.Up;
    [SerializeField, Range(0f, 1f)] private float initialPower = 0.5f;
    [SerializeField, Range(0f, 1f)] private float _minimalPower = 0.8f;
    [SerializeField] private RectTransform minimalPowerBar;
    [SerializeField] private Image powerBarImage;
    [SerializeField, Min(0f)] private float powerSpeed = 1f;
    [SerializeField, Min(0f)] private float powerSpeedFactor = 1.5f;
    private Direction currentPowerDirection;

    public float minimalPower => _minimalPower;
    public float currentPower => powerBarImage.fillAmount;

    void Start()
    {
        minimalPowerBar.anchoredPosition = new Vector2(minimalPowerBar.anchoredPosition.x, GetComponent<RectTransform>().rect.height * minimalPower);
        ResetPower();
    }

    public void ResetPower()
    {
        powerBarImage.fillAmount = initialPower;
        currentPowerDirection = initialDirection;
    }
    
    void Update()
    {
        powerBarImage.fillAmount += powerSpeed * Mathf.Exp(powerBarImage.fillAmount * powerSpeedFactor) * (int)currentPowerDirection * Time.deltaTime;

        if (powerBarImage.fillAmount == 1f)
            currentPowerDirection = Direction.Down;
        else if (powerBarImage.fillAmount == 0f)
            currentPowerDirection = Direction.Up;
    }
}
