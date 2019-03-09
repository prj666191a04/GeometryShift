using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Its starting to look like this class might not be needed. currently a class of type cconfig could be used directly
/// but we are keeping it for the time being for orginization It will be removed once the system is compleete if there is still no need for it.
/// </summary>
public class LevelInit : MonoBehaviour
{
    
    public GameObject playerPrefab;
    public Transform spawnPoint; //this will be replaced with save data later for open world map
    public GameObject parentObject;
    public bool mainMap = false;
    
    public CConfig cconfig;

    // Start is called before the first frame update
    void Start()
    {
        ConfigureSpawnPoint();
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
                    spawnPoint.position = LevelLoader.instance.GetDataCore().groupedData.playerData.GetPosition();
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
    


    // Update is called once per frame
    void Update()
    {
        
    }
    
}
