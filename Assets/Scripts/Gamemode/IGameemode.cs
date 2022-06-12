using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//надо, чтобы при остановке игрового режима, он возвращался в исходное состояние
public interface IGameemode
{
    public void Load(int code); //активирует все объекты
    public void Launch(); //запускается игра
    public void Stop(); //выключает все объекты
}
