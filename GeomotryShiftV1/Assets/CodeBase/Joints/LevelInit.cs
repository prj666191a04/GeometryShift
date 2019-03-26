//Author Atilla puskas
//Description: initializes a level and places the configured character on it.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelInit : MonoBehaviour
{
    
    public GameObject playerPrefab;
    public Transform spawnPoint; //this will be replaced with save data later for open world map
    public GameObject parentObject;
    public bool mainMap = false;
    public bool AutoStart = true;
    
    public CConfig cconfig;

    // Start is called before the first frame update
    void Start()
    {
        ConfigureSpawnPoint();
        if(AutoStart)
        {
            //copy pasted code to simply reduce function call overhead
            cconfig.SetupCharacter(playerPrefab, spawnPoint, parentObject);
            if (mainMap)
            {
                LevelLoader.instance.AutoSave();
            }
        }
    }

    public void Trigger()
    {
        cconfig.SetupCharacter(playerPrefab, spawnPoint, parentObject);
        if (mainMap)
        {
            LevelLoader.instance.AutoSave();
        }
    }


    void ConfigureSpawnPoint()
    {

        //if last exit point is not null that means we have exited from a level before and we have not just loaded the save file
            if (!LevelLoader.initialBoot && mainMap)
            {
                spawnPoint.position = LevelLoader.levelExitPoint;
                Debug.Log("LEVEL EXIT POINT");
            }
            else
            {
                LevelLoader.initialBoot = false;
                if (!LevelLoader.freshSave && mainMap)
                {
                    spawnPoint.position = LevelLoader.instance.dataCore.groupedData.playerData.GetPosition();
                    Debug.Log("USING SAVED SPAWN POINT");
                }
                else
                {
                    if(LevelLoader.freshSave)
                    {
                        LevelLoader.freshSave = false;
                    }
                    Debug.Log("USING DEFAULT SPAWN POINT");
                }
            }
    }
    


    
}
