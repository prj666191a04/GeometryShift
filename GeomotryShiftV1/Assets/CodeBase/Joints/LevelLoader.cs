using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{

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
        }
        else
        {
            Debug.LogError("More then one Level Loader ditected destroying exta... Please located and remove duplicate");
            Destroy(this);
        }
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
        GameObject.Instantiate(Level, EnvironmentContainer);
        Debug.Log("LevelLoader.cs " + System.Environment.NewLine + "finished levelLoad of " + Level.name);
        GeomotryShift.systemState = GeomotryShift.SystemState.WorldMap;

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
