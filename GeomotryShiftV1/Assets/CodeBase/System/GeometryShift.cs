using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//INPORTANT values ending with Prefab Should not be modified at runtime.

public class GeometryShift : MonoBehaviour
{
    public static Camera mainCamera;

    public static GeometryShift instance;

    //referance might be removed from this script but for now it seems like a good place to store it.
    public static CStatus playerStatus;

    public static SystemState systemState = SystemState.MainMenue;
    public enum SystemState
    {
        MainMenue,
        WorldMap,
        InLevel,
        Loading
    }


    //UI- this code might need to be moved to a ui manager class later on
    public GameObject mainMenuePrefab;
    public GameObject openWorldUiPrefab;
    private GameObject loadedUiSet;

    public InteractionUI interactionUI;

    void StateChange(SystemState state)
    {
        systemState = state;

        switch (state) {

            case SystemState.Loading:
                break;
            case SystemState.MainMenue:
                break;
            case SystemState.WorldMap:
                break;
            case SystemState.InLevel:
                break;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
