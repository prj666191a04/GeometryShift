//Author Atilla puskas
//Description: Recives messages from save slots

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveSlotSelect : MonoBehaviour
{
    public GameObject yesNoWindow;
    public GameObject newGameWindow;
    
    private SaveSlotYesNoPrompt prompt;
    private SaveSlotCreateSavePrompt namePrompt;
    public TMP_Text heading;

    public bool loadLevel = true;

    private void Start()
    {
        if(loadLevel)
        {
            heading.text = "LOAD GAME";
        }
        else
        {
            heading.text = "NEW GAME";
        }

        prompt = yesNoWindow.GetComponent<SaveSlotYesNoPrompt>();
        namePrompt = newGameWindow.GetComponent<SaveSlotCreateSavePrompt>();
    }

    public bool promptActive = false;

    public Color textUnselectedColor;
    public Color textSelectedColor;

    public Color iconSelectedColor;
    public Color iconUnselectedColor;

    public Color barSelectedColor;
    public Color barUnselectedColor;



    public void AskQuestion(string question, GroupedData saveData)
    {
        if (loadLevel)
        {
            prompt.messageText.text = question;
            prompt.dataToLoad = saveData;
            prompt.load = true;
            yesNoWindow.SetActive(true);
        }
        else
        {
            //create new save data
            prompt.messageText.text = question;
            prompt.dataToLoad = saveData;
            prompt.load = false;
            yesNoWindow.SetActive(true);
        }
    }
    public void NewGame(int slot)
    {
        newGameWindow.SetActive(true);
        namePrompt.saveSlot = slot;
        
    }

    public void BackToMain()
    {
        GeometryShift.instance.StateChange(GeometryShift.SystemState.MainMenue);
    }

}
