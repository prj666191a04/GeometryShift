using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//INPORTANT values ending with Prefab Should not be modified at runtime.

public class LevelLoader : MonoBehaviour
{

    public Camera mainCamera;
    public delegate void LoadEvent();
    public static event LoadEvent OnLevelLoaded;





    public static LevelLoader instance;

    //place holder in future custom type may be needed for world state
    private GameObject WorldState;

    
    public Transform EnvironmentContainer;
    public GameObject OpenWorldPreFab;


    private GameObject loadedEnvironment;
    
    



    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            GeomotryShift.mainCamera = this.mainCamera;
        }
        else
        {
            Debug.LogError("More then one Level Loader ditected destroying exta... Please located and remove duplicate");
            Destroy(this);
        }
    }


    private void OnEnable()
    {
        SubscribeEvents();
    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    void SubscribeEvents()
    {
        //todo: add events
    }
    void UnsubscribeEvents()
    {
        //todo: ^
    }
    private void Update()
    {
        
    }

    //Allows LoadLevel to be staticly called
    public static void LoadLevelExternalCall(GameObject Level)
    {
        instance.LoadLevel(Level);
    }
    public void LoadLevel(GameObject Level)
    {
        Debug.Log("LevelLoader.cs " + System.Environment.NewLine + "Starting levelLoad of " + Level.name);
        GeomotryShift.systemState = GeomotryShift.SystemState.Loading;
        UnloadWorld();
        loadedEnvironment = GameObject.Instantiate(Level, EnvironmentContainer);
        Debug.Log("LevelLoader.cs " + System.Environment.NewLine + "finished levelLoad of " + Level.name);
        GeomotryShift.systemState = GeomotryShift.SystemState.WorldMap;
        if(OnLevelLoaded != null)
        {
            OnLevelLoaded();
        }

    }
    void UnloadWorld()
    {
        if(loadedEnvironment != null)
        {
            GameObject.Destroy(loadedEnvironment);
            loadedEnvironment = null;
        }

    }

    void LoadWorld()
    {

    }

}
