//Author Atilla Puskas
//Descriotion: Displays help info to the player.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataNode : CInteractable
{
    public int id = 0;
    public GameObject infoPackage;
    public override void Respond()
    {
        GeometryShift.instance.interactionUI.Apear(interactText_);
    }

    public override void Trigger()
    {
        GeometryShift.instance.DspInfo(id);
        
    }


}
