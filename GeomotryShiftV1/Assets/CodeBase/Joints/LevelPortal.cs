using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPortal : CInteractable
{
    public GameObject levelObject;
    

    public Transform exitLocation;
    // Start is called before the first frame update

    public override void Trigger()
    {
        LevelLoader.levelExitPoint = exitLocation.position;
        Debug.Log("exit position set to " + LevelLoader.levelExitPoint);
        LevelLoader.LoadLevelExternalCall(levelObject);
    }

    public override void Respond()
    {
        GeometryShift.instance.interactionUI.Apear(interactText_);
    }

}
