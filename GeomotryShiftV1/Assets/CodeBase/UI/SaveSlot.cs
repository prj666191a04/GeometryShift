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

    private Color targetBarColor;
    private Color targetTextColor;
    private Color TargetIconColor;

    public GroupedData saveData;

    public int slotId;


    // Start is called before the first frame update
    void Start()
    {
        CollectSlotData();
    }


    void CollectSlotData()
    {
        try
        {
            saveData = SaveSystem.LoadGameData(slotId);
            nameText.text = saveData.playerData.name;
            System.TimeSpan time = System.TimeSpan.FromSeconds(saveData.playerData.playTime);
            string timeString = time.ToString(@"hh\:mm\:ss\:fff");
            playTime.text = "Play time: " + timeString;
            dataText.enabled = false;
        }
        catch
        {
            nameText.text = "no data";
            playTime.text = "Play time: no data";
            dataText.enabled = true;
        }
    }

    void animateToTarget()
    {
        nameText.color = Color.Lerp(nameText.color, targetTextColor, Time.deltaTime *2);
        Bar.color = Color.Lerp(Bar.color, targetBarColor, Time.deltaTime*2);
        icon.color = Color.Lerp(icon.color, TargetIconColor, Time.deltaTime*2);
    }

    public void SetSelected()
    {
        targetBarColor = slotSelector.barSelectedColor;
        TargetIconColor = slotSelector.iconSelectedColor;
        targetTextColor = slotSelector.textSelectedColor;
        selected = this;

    }
    public void SetUnselected()
    {
        targetBarColor = slotSelector.barUnselectedColor;
        TargetIconColor = slotSelector.iconUnselectedColor;
        targetTextColor = slotSelector.textUnselectedColor;

    }

}
