using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMTermination : MonoBehaviour
{
    public event Action<int> gameModeTermination; //� ����� ����� ����������� ������� �����: 0 - ��������, 1- �������, 2 - �������������, 3 - � ����, 4-... - ������� �� ������

    protected void EndGamemode(int code) //������ ������ �� ����� �� �������� ������
    {
        gameModeTermination?.Invoke(code);
    }
}
