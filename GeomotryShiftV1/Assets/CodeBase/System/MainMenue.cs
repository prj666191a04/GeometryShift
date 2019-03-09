using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenue : MonoBehaviour
{
    public string dataPath;
    public string presistantDataPath;

    public GameObject slotSelector;
    public GameObject slotSelectorN;

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

    //tmp
    public void NewGameBtn()
    {
        GeometryShift.instance.DistroyLoadedUISet();
        GeometryShift.instance.loadedUiSet = Instantiate(slotSelectorN, GeometryShift.instance.activeUIContainer.transform);
    }
    //tmp
    public void ContinueBtn()
    {
        GeometryShift.instance.DistroyLoadedUISet();      
        GeometryShift.instance.loadedUiSet = Instantiate(slotSelector, GeometryShift.instance.activeUIContainer.transform);
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

