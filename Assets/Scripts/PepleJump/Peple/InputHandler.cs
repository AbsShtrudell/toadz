using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Zenject.Inject] Peple peple;

    [SerializeField, Min(1)] float _shootSector = 5;
    public float shootSector => _shootSector;

    [System.Flags] public enum Type
    { None = 1 ,Move_Left = 2, Move_Right = 4, Shoot = 8}

    [HideInInspector] public Type ActiveInput;

    private void Update()
    {
        ActiveInput = Type.None;

        for (int i =0; i < Input.touchCount; i++)
        {
            ActiveInput = ActiveInput | TouchSector(Input.GetTouch(i));
        }
    }

    private Type TouchSector(Touch touch)
    {
        if (touch.position.y > Screen.height / 5)
        {
            if(touch.position.x < Screen.width / 2 ) return Type.Move_Left;
            else return Type.Move_Right;
        }
        else return Type.Shoot;
    }
}
