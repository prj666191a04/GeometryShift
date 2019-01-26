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
        Debug.Log("portal triggered");
        LevelLoader.levelExitPoint = exitLocation.position;
        LevelLoader.LoadLevelExternalCall(levelObject);
    }



}
