using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotSelect : MonoBehaviour
{
    public GameObject yesNoWindow;
    public GameObject newGameWindow;
    private SaveSlotYesNoPrompt prompt;
    private SaveSlotCreateSavePrompt namePrompt;

    public bool loadLevel = true;

    private void Start()
    {
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

}
