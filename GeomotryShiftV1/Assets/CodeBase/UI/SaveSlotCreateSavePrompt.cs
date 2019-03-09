//Author Atilla puskas
//Description: behavoir for creating a new save file
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotCreateSavePrompt : MonoBehaviour
{
    public SaveSlotSelect selector;
    public Text nameText;
    public Text limutText;
    public InputField nameField;
    public int saveSlot;

    private int maxChars = 16;

    // Start is called before the first frame update
    void Start()
    {
        nameField.characterLimit = maxChars;
        nameField.Select();
        nameField.ActivateInputField();
    }
    private void OnEnable()
    {
        nameField.Select();
        nameField.ActivateInputField();
    }

    public bool ValidateName()
    {
        if (nameText.text.Length > 0 && nameText.text.Length < maxChars && !nameText.text.StartsWith(" "))
        {
           
            return true;
        }
        else
        {
            return false;
        }
    }
    public void UpdateLengthText()
    {
        int num = nameField.text.Length;
        if (num < 10)
        {
            limutText.text = "0" + num.ToString() + "/" + maxChars;
        }
        else
        {
            limutText.text = num.ToString() + "/" + maxChars;
        }
    }
    public void CreateSaveSlot()
    {
        if (ValidateName())
        {
            string name = nameText.text;

            Leveldata[] newLevelData = new Leveldata[DataCore.levelCount];
            for (int i = 0; i < newLevelData.Length; i++)
            {
                newLevelData[i] = new Leveldata(i, -1);
            }
            WorldState newWorldState = new WorldState(newLevelData);
            //TODO: Inventory
            PlayerData newPlayerData = new PlayerData(name, 0, Vector3.zero);

            GroupedData newGroupedData = new GroupedData(newPlayerData, newWorldState, saveSlot);

            DataCore newDatacore = new DataCore(newGroupedData);

            LevelLoader.instance.InitWorldState(newDatacore);
            GeometryShift.instance.StartSessionTimer();
            GeometryShift.instance.sessionTimer.Init();
            LevelLoader.freshSave = true;
            Debug.LogWarning("firstINit");
            LevelLoader.instance.LoadWorldMap();
        }
        else
        {
            Debug.Log("name invalid");
        }
    }
    public void NoClick()
    {
        selector.promptActive = false;
        this.gameObject.SetActive(false);
    }
}
