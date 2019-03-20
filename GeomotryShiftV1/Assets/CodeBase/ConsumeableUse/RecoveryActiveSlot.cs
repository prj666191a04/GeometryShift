using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryActiveSlot : ConsumeableActiveSlot
{
    public override void Activate()
    {
        if(qty_ > 0)
        {
            GeometryShift.playerStatus.RecoverItem();
            qty_--;
        }
    }
}
