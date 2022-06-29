using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRClear : ITrigger
{
    DUIController controller;

    public TRClear(DUIController controller)
    {
        this.controller = controller;
    }

    public void Call()
    {
        controller.Clear();
    }
}
