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
    private DataCore dataCore;

    public Transform EnvironmentContainer;
    public GameObject openWorldPreFab;
    private GameObject loadedEnvironment;
    

    //Data related functions

    public void InitWorldState(DataCore core)
    {
        dataCore = core;
    }
    public void UpdateLevelStatus(int level, int code)
    {
        dataCore.groupedData.worldState.levelState[level].Update(code);
    }
    public DataCore GetDataCore()
    {
        return dataCore;
    }

    public void SetGroupedData(GroupedData data)
    {
        dataCore.groupedData = data;
    }

    //^Data related functions

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
        LevelBase.OnLevelFailed += ReturnFromFailedLevel;
    }
    void UnsubscribeEvents()
    {
        LevelBase.OnLevelCompleeted -= ReturnFromCompleetedLevel;
        LevelBase.OnLevelFailed -= ReturnFromFailedLevel;
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
        UpdateLevelStatus(id, code);
        //TODO: Call auto save, (not yet implemented)
        //Possible Temporary
        SaveSystem.SaveGameData(dataCore.groupedData.slot);
    }

    private void ReturnFromFailedLevel()
    {
        GeometryShift.instance.StateChange(GeometryShift.SystemState.Loading);
        UnloadWorld();
        loadedEnvironment = GameObject.Instantiate(openWorldPreFab, EnvironmentContainer);
        GeometryShift.instance.StateChange(GeometryShift.SystemState.WorldMap);
    }

    public void ReturnToMainMenue()
    {
        UnloadWorld();
        GeometryShift.instance.StateChange(GeometryShift.SystemState.MainMenue);
        initialBoot = true;
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



}
