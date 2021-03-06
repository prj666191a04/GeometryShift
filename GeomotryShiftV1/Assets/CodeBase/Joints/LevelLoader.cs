﻿//Author Atilla puskas
//Description: Loads levels and manages much of the game state


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
    public static bool freshSave = false;

    //place holder in future custom type may be needed for world state
    public DataCore dataCore;

    public Transform EnvironmentContainer;
    public GameObject openWorldPreFab;
    private GameObject loadedEnvironment;
    private GameObject playerGameObject;

    //Data related functions

    public LayerMask wMapColLayer;    


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
    public void AutoSave()
    {
        dataCore.groupedData.playerData.playTime = GeometryShift.instance.sessionTimer.EndSession();
        Vector3 savePostion = GeometryShift.playerStatus.transform.position;
        savePostion.y += 0.2f;
        Debug.Log("AutoSave called player pos: " + savePostion.ToString());
        dataCore.groupedData.playerData.SetPosition(savePostion);
        SaveSystem.SaveGameData(dataCore.groupedData.slot);
    }
    public void SoftAutoSave()
    {
        SaveSystem.SaveGameData(dataCore.groupedData.slot);
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
        AutoSave();      
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
        GeometryShift.instance.StateChange(GeometryShift.SystemState.Loading);
        UnloadWorld();
        loadedEnvironment = GameObject.Instantiate(Level, EnvironmentContainer);
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
