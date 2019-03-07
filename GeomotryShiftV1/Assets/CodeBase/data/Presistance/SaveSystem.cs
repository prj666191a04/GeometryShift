using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem 
{
    public static void SaveGameData(int slot)
    {
        string filePath = GeometryShift.instance.GetDataPath() + "/slot" + slot.ToString() + ".save"; 
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }
        string json = setUpSaveData();
        File.WriteAllText(filePath, json);
    }

    private static string setUpSaveData()
    {
        GroupedData saveData = LevelLoader.instance.GetDataCore().groupedData;
        string jsonData = JsonUtility.ToJson(saveData);
        return jsonData;
    }
    //Creates a home for the save data, This must be called at least once in the lifetime of the program prior to LoadGameData being called!
    //Except when creating a brand new save file.
    public static void InitilizeDataStructure()
    {
        DataCore newDatacore = new DataCore();
        LevelLoader.instance.InitWorldState(newDatacore);
    }

    public static GroupedData LoadGameData(int slot)
    {
        string filePath = GeometryShift.instance.GetDataPath() + "/slot" + slot.ToString() + ".save";
        string json;

        if (File.Exists(filePath))
        {
            try
            {
                json = File.ReadAllText(filePath);
                GroupedData saveData = JsonUtility.FromJson<GroupedData>(json);
                saveData.worldState.ComfirmArraySize();
                return saveData;
            }
            catch
            {
                return null;
            }
      
        }
        else
        {
            Debug.Log("File is missing " + filePath);
            return null;
        }
    }


}



