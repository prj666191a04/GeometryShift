//Author Atilla puskas
//Description: Controls the main menue of the game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenue : MonoBehaviour
{
    public string dataPath;
    public string presistantDataPath;

    public GameObject slotSelector;
    public GameObject slotSelectorN;

    public Button continueButton;
    bool continueHasData;

    private void Start()
    {
        continueHasData = true;
        continueHasData = HasData();
            if(!continueHasData)
            { 
            continueButton.GetComponent<Image>().color = Color.red;
            }
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
        if (continueHasData)
        {
            SystemSounds.instance.UIClick();
            GeometryShift.instance.DistroyLoadedUISet();
            GeometryShift.instance.loadedUiSet = Instantiate(slotSelector, GeometryShift.instance.activeUIContainer.transform);
        }
        else
        {
            SystemSounds.instance.UIError();
        }
    }


    public bool HasData()
    {
        for (int i = 0; i < 3; i++)
        {
            GroupedData tmp = SaveSystem.LoadGameData(i);
            if(tmp != null)
            {
                return true;
            }
        }
        return false;
    }
}

