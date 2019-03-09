//Author Atilla puskas
//Description: behavoiur for a save slot

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{

    
    public SaveSlotSelect slotSelector;

    public static SaveSlot selected;

    public Image icon;
    public Image data;
    public Image Bar;
    public Text nameText;
    public Text dataText;
    public Text playTime;

    public Color targetBarColor;
    public Color targetTextColor;
    public Color TargetIconColor;


    public GroupedData saveData;

    public int slotId;
    bool hasData = false;


    // Start is called before the first frame update
    void Start()
    {
        CollectSlotData();
        if(slotId == 0)
        {
            selected = this;
            SetSelected();
        }
        else
        {
            SetUnselected();
        }


    }

    public void Click()
    {
        if (!slotSelector.promptActive)
        {
            if (slotSelector.loadLevel)
            {
                if (hasData)
                {
                    slotSelector.promptActive = true;
                    slotSelector.AskQuestion("Start game with save slot " + (slotId +1).ToString() +" ?", saveData);
                }
                else
                {
                    Bar.color = Color.red;
                }
            }
            else
            {
                if(hasData)
                {
                    slotSelector.promptActive = true;
                    slotSelector.AskQuestion("Overwrite save for slot " +(slotId+1).ToString()+"?"+ System.Environment.NewLine + "Warning: this cannot be undone", saveData);
                }
                else
                {
                    slotSelector.NewGame(slotId);
                }
            }
        }
    }

    private void Update()
    {
        AnimateToTarget();
    }

    void CollectSlotData()
    {
        try
        {
            saveData = SaveSystem.LoadGameData(slotId);
            nameText.text = saveData.playerData.name;
            System.TimeSpan time = System.TimeSpan.FromSeconds(saveData.playerData.playTime);
            string timeString = time.ToString(@"hh\:mm\:ss");
            playTime.text = "Play time: " + timeString;
            dataText.enabled = false;
            hasData = true;
        }
        catch
        {
            nameText.text = "no data";
            playTime.text = "Play time: no data";
            dataText.enabled = true;
            hasData = false;
        }
    }

    void AnimateToTarget()
    {
        if (Bar.color != targetBarColor)
        {
            nameText.color = Color.Lerp(nameText.color, targetTextColor, Time.deltaTime * 5);
            Bar.color = Color.Lerp(Bar.color, targetBarColor, Time.deltaTime * 5);
            icon.color = Color.Lerp(icon.color, TargetIconColor, Time.deltaTime * 5);
        }
    }

    public void SetSelected()
    {
        if (!slotSelector.promptActive)
        {
            selected.SetUnselected();
            targetBarColor = slotSelector.barSelectedColor;
            TargetIconColor = slotSelector.iconSelectedColor;
            targetTextColor = slotSelector.textSelectedColor;
            selected = this;
        }
    }
    
    protected void SetUnselected()
    {
        targetBarColor = slotSelector.barUnselectedColor;
        TargetIconColor = slotSelector.iconUnselectedColor;
        targetTextColor = slotSelector.textUnselectedColor;
    }

}
