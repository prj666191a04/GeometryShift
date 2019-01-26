using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemSlot : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
    public Text itemName;
    public Text itemDesc;
    public Image icon;

    private TestItem item;
    private Inventory inventory;

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
        //inventory.tryTransferItem(item);
    }


}
