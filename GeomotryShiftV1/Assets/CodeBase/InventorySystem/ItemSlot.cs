using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Semi smart item slot
//Holds info of item for smarter inventory
//Decription is accessed through the item slot its self

public class ItemSlot : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
    public Text itemName;
    public Text itemDesc;
    public Image icon;
    public GameObject descriptionPannel;

    private TestItem item;
    private Inventory inventory;
    private descriptionBox description;
    

    //public GameObject descriptionPanel; // UI Panel - used to show and hide the UI

    void Start()
    {
        button.onClick.AddListener(handleClick);
    }

    public void setup(TestItem currentIten, Inventory currentInventory)
    {
        item = currentIten;
        itemName.text = item.itemName;
        itemDesc.text = item.value.ToString();
        icon.sprite = item.icon;

        inventory = currentInventory;

    }

    public void handleClick()
    {
        inventory.loadDescription(item);
    }


}
