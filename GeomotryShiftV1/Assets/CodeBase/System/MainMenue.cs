using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenue : MonoBehaviour
{
    public string dataPath;
    public string presistantDataPath;

    private void Start()
    {
        NewGame();  
      
    }

    public void LoadWorld()
    {
        LevelLoader.instance.LoadWorldMap();
        
    }

    public void QuitGame()
    {
        GeometryShift.instance.QuitGame();
    }


    public void NewGame()
    {
  
        CreateNewSaveSlot(0, "Atilla");
    }

    //Generates a savefile for a brand new game
    public void CreateNewSaveSlot(int slot, string name)
    {
        Leveldata[] newLevelData = new Leveldata[10];
        for (int i = 0; i < newLevelData.Length; i++)
        {
            newLevelData[i] = new Leveldata(i, -1);
        }
        WorldState newWorldState = new WorldState(newLevelData);
        //TODO: Inventory
        PlayerData newPlayerData = new PlayerData(name, 0, Vector3.zero);

        GroupedData newGroupedData = new GroupedData(newPlayerData, newWorldState, slot);

        DataCore newDatacore = new DataCore(newGroupedData);

        LevelLoader.instance.InitWorldState(newDatacore);

    }

}

