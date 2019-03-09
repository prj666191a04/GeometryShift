using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotYesNoPrompt : MonoBehaviour
{
    public SaveSlotSelect selector;

    public GroupedData dataToLoad;
    public Text messageText;

    public bool load;

    public void NoClick()
    {
        selector.promptActive = false;
        this.gameObject.SetActive(false);
    }

    public void YesClick()
    {
        if (load)
        {
            SaveSystem.InitilizeDataStructure();
            LevelLoader.instance.SetGroupedData(dataToLoad);
            GeometryShift.instance.StartSessionTimer();
            GeometryShift.instance.sessionTimer.Init();
            LevelLoader.instance.LoadWorldMap();
            
        }//overwrite
        else
        {
            //call New Game Window
            selector.NewGame(dataToLoad.slot);
            this.gameObject.SetActive(false);
        }
    }
}
