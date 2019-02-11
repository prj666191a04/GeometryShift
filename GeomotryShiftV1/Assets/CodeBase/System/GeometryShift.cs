using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//INPORTANT values ending with Prefab Should not be modified at runtime.

public class GeometryShift : MonoBehaviour
{
    //tmp
    public static Camera mainCamera;

    //tmp
    public CameraControllerA cameraController;

    public static GeometryShift instance;

    //referance might be removed from this script but for now it seems like a good place to store it.
    public static CStatus playerStatus;


  

    private static SystemState systemState = SystemState.MainMenue;
    public enum SystemState
    {
        MainMenue,
        WorldMap,
        InLevel,
        Loading
    }


    //UI- this code might need to be moved to a ui manager class later on
    public Transform interactionUIContainer;
    public Transform activeUIContainer;


    public GameObject mainMenuePrefab;
    public GameObject openWorldUiPrefab;

    private GameObject loadedUiSet;


    public InteractionUI interactionUI;

    public static SystemState GetSystemState()
    {
        return systemState;
    }

    public void StateChange(SystemState state)
    {
        SystemState prevoiusState = systemState;
        systemState = state;

        
        switch (state) {

            case SystemState.Loading:
                DistroyLoadedUISet();
                //TODO: Display Loading screen (does not yet exist)
                break;
            case SystemState.MainMenue:
                DistroyLoadedUISet();
                break;
            case SystemState.WorldMap:
                DistroyLoadedUISet();
                break;
            case SystemState.InLevel:
                DistroyLoadedUISet();
                break;
        }

    }

    private void ChangeUISet( GameObject UISet)
    {

        DistroyLoadedUISet();
        Instantiate(UISet, activeUIContainer);
        
    }
    private void DistroyLoadedUISet()
    {
        if(loadedUiSet != null)
        {
            Destroy(loadedUiSet);
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

        Initalize();
    }

    //Starts the game
    private void Initalize()
    {
        //TODO: Check if user agreed to tos

        //TODO: If they have beginloading the game to the main menue if not prompt them to read and agree


        //TODO: Read save file names into memory and prepare them for use

        loadedUiSet = Instantiate(mainMenuePrefab, activeUIContainer);



    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
