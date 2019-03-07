using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenue : MonoBehaviour
{
    public string dataPath;
    public string presistantDataPath;

    private void Start()
    {
       
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

    //tmp
    public void NewGameBtn()
    {
        CreateNewSaveSlot(0, "Atilla");
        LevelLoader.instance.LoadWorldMap();

    }
    //tmp
    public void ContinueBtn()
    {
        SaveSystem.InitilizeDataStructure();
        GroupedData loadedData = SaveSystem.LoadGameData(0);
        if (loadedData != null)
        {
            LevelLoader.instance.SetGroupedData(loadedData);
            LevelLoader.instance.LoadWorldMap();
        }
            
        
    }


    //Generates a savefile for a brand new game
    public void CreateNewSaveSlot(int slot, string name)
    {
        Leveldata[] newLevelData = new Leveldata[DataCore.levelCount];
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
    public void LoadSlot(int slot)
    {
       SaveSystem.LoadGameData(slot);

    }
    public void FetchSlots()
    {

    }
}

