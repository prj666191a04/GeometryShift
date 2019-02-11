using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//INPORTANT values ending with Prefab Should not be modified at runtime.

public class LevelLoader : MonoBehaviour
{

    public delegate void LoadEvent();
    public static event LoadEvent OnLevelLoaded;

    public static LevelLoader instance;
    public static Vector3 levelExitPoint;
    public static bool initialBoot = true;

    //place holder in future custom type may be needed for world state
    private GameObject WorldState;

    public Transform EnvironmentContainer;
    public GameObject openWorldPreFab;
    private GameObject loadedEnvironment;
    
  
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            //GeometryShift.mainCamera = this.mainCamera;
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
        LevelBase.OnLevelCompleeted += ReturnFromCompleetedLevel;
    }
    void UnsubscribeEvents()
    {
        LevelBase.OnLevelCompleeted += ReturnFromCompleetedLevel;
    }
    private void Update()
    {
        
    }

    //Allows LoadLevel to be staticly called
    public static void LoadLevelExternalCall(GameObject Level)
    {
        instance.LoadLevel(Level);
    }
    
    private void ReturnFromCompleetedLevel(int id, int code)
    {
        Debug.Log("LevelLoader.cs: returning from level with id of " + id);
        GeometryShift.instance.StateChange(GeometryShift.SystemState.Loading);
        UnloadWorld();
        loadedEnvironment = GameObject.Instantiate(openWorldPreFab, EnvironmentContainer);

        GeometryShift.instance.StateChange(GeometryShift.SystemState.WorldMap);
        //TODO: Save id and completion code to save state object

        //TODO: Call auto save, (not yet implemented)



    }

    private void ReturnFromFailedLevel()
    {
        //Todo:: implement
    }


    public void LoadWorldMap(int map = 0)
    {
        GeometryShift.instance.StateChange(GeometryShift.SystemState.Loading);
        UnloadWorld();
        loadedEnvironment = GameObject.Instantiate(openWorldPreFab, EnvironmentContainer);
        GeometryShift.instance.StateChange(GeometryShift.SystemState.WorldMap);

    }

    public void LoadLevel(GameObject Level)
    {
        Debug.Log("LevelLoader.cs " + System.Environment.NewLine + "Starting levelLoad of " + Level.name);
        GeometryShift.instance.StateChange(GeometryShift.SystemState.Loading);
        UnloadWorld();
        loadedEnvironment = GameObject.Instantiate(Level, EnvironmentContainer);
        Debug.Log("LevelLoader.cs " + System.Environment.NewLine + "finished levelLoad of " + Level.name);
        GeometryShift.instance.StateChange(GeometryShift.SystemState.InLevel);
        if (OnLevelLoaded != null)
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
