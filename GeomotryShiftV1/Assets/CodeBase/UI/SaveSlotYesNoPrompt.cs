//Author Atilla puskas
//Description: confirmation of a user action


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
        SystemSounds.instance.UIClick();
        selector.promptActive = false;
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            YesClick();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            NoClick();
        }
    }

    public void YesClick()
    {
        if (load)
        {
            SystemSounds.instance.UIAdavance();
            SaveSystem.InitilizeDataStructure();
            LevelLoader.instance.SetGroupedData(dataToLoad);
            GeometryShift.instance.StartSessionTimer();
            GeometryShift.instance.sessionTimer.Init();
            LevelLoader.instance.LoadWorldMap();
            
        }//overwrite
        else
        {
            SystemSounds.instance.UIClick();
            //call New Game Window
            selector.NewGame(dataToLoad.slot);
            this.gameObject.SetActive(false);
        }
    }
}
