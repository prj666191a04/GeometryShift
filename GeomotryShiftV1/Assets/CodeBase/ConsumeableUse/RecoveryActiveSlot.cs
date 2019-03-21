using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecoveryActiveSlot : ConsumeableActiveSlot
{
    public Text counter;
    public override void Activate()
    {
        if(qty_ > 0)
        {
            Debug.Log("recovery activated");
            GeometryShift.playerStatus.RecoverItem();
            qty_--;
            UpdateCounter();
        }
    }

    void UpdateCounter()
    {
        if(qty_ > 9)
        {
            counter.text = qty_.ToString();
        }
        else
        {
            counter.text = "0" + qty_.ToString();
        }
    }
}
