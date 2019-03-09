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

    private void Start()
    {
        if(HasData())
        {
            continueButton.enabled = true;
        }
        else
        {
            continueButton.enabled = false;
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

        GeometryShift.instance.DistroyLoadedUISet();      
        GeometryShift.instance.loadedUiSet = Instantiate(slotSelector, GeometryShift.instance.activeUIContainer.transform);
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

