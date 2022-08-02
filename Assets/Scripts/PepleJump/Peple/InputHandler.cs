using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Zenject.Inject] Peple peple;
    [Zenject.Inject] Joystick joystick;


    [SerializeField, Min(1)] float _shootSector = 7;

    public float shootSector => _shootSector;
    public float moveSpeed
    { get { return Mathf.Clamp(Mathf.Abs(joystick.Direction.x), 0.1f, 1f); } }

    [System.Flags] public enum Type
    { None = 1 ,Move_Left = 2, Move_Right = 4, Shoot = 8}

    [HideInInspector] public Type ActiveInput;

    private void Update()
    {
        ActiveInput = Type.None;

        if (Input.GetButton("Fire1"))
        {
            if (joystick.Direction.x < 0) ActiveInput = ActiveInput | Type.Move_Left;
            else  if(joystick.Direction.x > 0) ActiveInput = ActiveInput | Type.Move_Right;
        }
    }
}
