using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataNode : CInteractable
{
    public GameObject infoPackage;
    public override void Respond()
    {
        GeometryShift.instance.interactionUI.Apear(interactText_);
    }

    public override void Trigger()
    {
        Debug.Log("Data node triggered");
    }


}
