using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Smart inventroy allowed to add and remove items


[System.Serializable]
public class TestItem
{
    public string itemName;
    public Sprite icon;
    public float value = 1f;
}



public class Inventory : MonoBehaviour
{
    //Main inventory variable
    public List<TestItem> itemlist;
    public SimpleObjectPool buttonpool;
    public Transform itemContenetPanel;


    //Desc stuff
    public SimpleObjectPool descriptionPool;
    public Transform descContenetPanel;

    // Start is called before the first frame update
    void Start()
    {
        refreshDispaly();
    }

    public void refreshDispaly()
    {
        //goldDisplay.text = "Gold: " + gold.ToString();
        removeButton();
        addButton();

    }

    private  void addButton()
    {
        for (int i = 0; i < itemlist.Count; i++)
        {
            TestItem item = itemlist[i];
            GameObject newbutton = buttonpool.GetObject();//grabs an opbject from the pool to use
            newbutton.transform.SetParent(itemContenetPanel); // assigns the object to the inventory panel 
            
            ItemSlot itemSlot = newbutton.GetComponent<ItemSlot>(); // gets the new game instance 
            itemSlot.setup(item, this); // sents to inner function to assign values
        }
    }

    private void removeButton()
    {
        while (itemContenetPanel.childCount > 0)
        {
            GameObject toRemove = itemContenetPanel.transform.GetChild(0).gameObject; //Aslong as there is an object in the invetory there will always b a child (0)
            buttonpool.ReturnObject(toRemove); // Returns the object back to the object pool for later use
        }
    }

    
    public void loadDescription(TestItem item)
    {

        
        if (descContenetPanel.childCount > 0)
        {
            GameObject toRemove = descContenetPanel.transform.GetChild(0).gameObject; //Aslong as there is an object in the invetory there will always b a child (0)
            descriptionPool.ReturnObject(toRemove); // Returns the object back to the object pool for later use
        }

        GameObject newDescPoolObj = descriptionPool.GetObject();
        newDescPoolObj.transform.SetParent(descContenetPanel);

        descriptionBox newDesc = newDescPoolObj.GetComponent<descriptionBox>();
        newDesc.setup(item);
    }


    private void addItem(TestItem itemToAdd, Inventory inventory)
    {
        inventory.itemlist.Add(itemToAdd);
    }
    private void removeItem(TestItem itemToRemove, Inventory inventory)
    {
        for (int i = inventory.itemlist.Count - 1; i >= 0; i--)//Allows for safe item removal. Will not mess up the count and cause an overflow/out of index crash
        {
            if (inventory.itemlist[i] == itemToRemove)
                inventory.itemlist.RemoveAt(i);
        }
    }

}
