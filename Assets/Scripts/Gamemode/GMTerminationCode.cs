using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMTermination : MonoBehaviour
{
    public event Action<int> gameModeTermination; //с каким кодом завершается игровой режим: 0 - проиграл, 1- выиграл, 2 - перезагрузить, 3 - в меню, 4-... - зависит от режима

    protected void EndGamemode(int code) //кидает запрос на выход из игрового режима
    {
        gameModeTermination?.Invoke(code);
    }
}
