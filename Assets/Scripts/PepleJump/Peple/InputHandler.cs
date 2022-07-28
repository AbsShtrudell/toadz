using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Zenject.Inject] Peple peple;
    [Zenject.Inject] Joystick joystick;


    [SerializeField, Min(1)] float _shootSector = 7;

    private bool hold = false;
    private Vector3 clickPosition = Vector3.zero;
    private Type clickType = Type.None;

    public float shootSector => _shootSector;
    public float moveSpeed
    { get { return Mathf.Abs((Screen.width / 2) - clickPosition.x) / (Screen.width / 2) ; } }

    [System.Flags] public enum Type
    { None = 1 ,Move_Left = 2, Move_Right = 4, Shoot = 8}

    [HideInInspector] public Type ActiveInput;

    private void Update()
    {
        ActiveInput = Type.None;

        if (Input.GetButton("Fire1"))
        {
            Type input = TouchSector((Vector2)Input.mousePosition);

            ActiveInput = ActiveInput | input;

            if (input == Type.Move_Left || input == Type.Move_Right)
            {
                //clickType = input;
                clickPosition = Input.mousePosition;
                //hold = true;
            }

            //if(input == Type.None)
            //{
            //    if (joystick.Direction.x < 0) ActiveInput = ActiveInput | Type.Move_Left;
            //    else ActiveInput = ActiveInput | Type.Move_Right;
            //}
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            Type input = TouchSector(Input.GetTouch(i).position);

            ActiveInput = ActiveInput | input;

            if (input == Type.Move_Left || input == Type.Move_Right)
            {
                //clickType = input;
                clickPosition = Input.mousePosition;
                //hold = true;
            }
        }

        //if (!ActiveInput.HasFlag(Type.Move_Left) && !ActiveInput.HasFlag(Type.Move_Right))
        //{
        //   // hold = false;
        //    //clickPosition = Vector3.zero;
        //}
    }

    private Type TouchSector(Vector2 position)
    {
        if (position.y > Screen.height / 5)
        {
            ////return Type.None;
            //if (hold)
            //    if (Mathf.Abs(clickPosition.x - position.x) > 10)
            //    {
            //        if (position.x <= clickPosition.x) return Type.Move_Left;
            //        else return Type.Move_Right;
            //    }
            //    else return clickType;
            //else
            //    if (position.x < Screen.width / 2) return Type.Move_Left;
            //else return Type.Move_Right;

            if (position.x < Screen.width / 2) return Type.Move_Left;
            else return Type.Move_Right;
        }
        else return Type.Shoot;
    }
}
