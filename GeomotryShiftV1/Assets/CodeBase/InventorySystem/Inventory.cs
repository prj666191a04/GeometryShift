using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class TestItem
{
    public string itemName;
    public Sprite icon;
    public float value = 1f;
}



public class Inventory : MonoBehaviour
{

    public List<TestItem> itemlist;
    public Transform contenetPanel;
    public Inventory otherinventory;
    public Text goldDisplay;
    public SimpleObjectPool buttonpool;
    public float gold = 20f;
    // Start is called before the first frame update
    void Start()
    {
        refreshDispaly();
    }


    public void refreshDispaly()
    {
        goldDisplay.text = "Gold: " + gold.ToString();
        removeButton();
        addButton();

    }

    private  void addButton()
    {
        for (int i = 0; i < itemlist.Count; i++)
        {
            TestItem item = itemlist[i];
            GameObject newbutton = buttonpool.GetObject();//grabs an opbject from the pool to use
            newbutton.transform.SetParent(contenetPanel); // assigns the object to the inventory panel 

            ItemSlot itemSlot = newbutton.GetComponent<ItemSlot>(); // gets the new game instance 
            itemSlot.setup(item, this); // sents to inner function to assign values
        }
    }

    private void removeButton()
    {
        while (contenetPanel.childCount > 0)
        {
            GameObject toRemover = transform.GetChild(0).gameObject; //Aslong as there is an object in the invetory there will always b a child (0)
            buttonpool.ReturnObject(toRemover); // Returns the object back to the object pool for later use
        }
    }

    public void tryTransferItem(TestItem item)
    {
        if (otherinventory.gold >= item.value)
        {
            gold += item.value;
            otherinventory.gold -= item.value;

            addItem(item, otherinventory);
            removeItem(item, this);

            refreshDispaly();
            otherinventory.refreshDispaly();
        }
    }

    private void addItem(TestItem itemToAdd, Inventory inventory)
    {
        inventory.itemlist.Add(itemToAdd);
    }
    private void removeItem (TestItem itemToRemove, Inventory inventory)
    {
        for (int i = inventory.itemlist.Count -1;  i >=0; i--)//Allows for safe item removal. Will not mess up the count and cause an overflow/out of index crash
        {
            if (inventory.itemlist[i] == itemToRemove)
                inventory.itemlist.RemoveAt(i);
        }
    }



}
