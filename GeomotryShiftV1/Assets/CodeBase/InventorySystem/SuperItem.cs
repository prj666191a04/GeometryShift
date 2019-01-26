using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperItem : MonoBehaviour
{
    private string itemName;
    private string itemDescription;
    public virtual void useItem() { } // Virtual function for items use. EVERY ITEM MUST MAKE ITS OWN USE FUNCTION

    public SuperItem() {
        this.itemName = "";
        this.itemDescription = "";
    }

    public SuperItem(string name, string desc) // SuperClass constructor. Items MUST have an item name and  description. 
    {
        this.itemName = name;
        this.itemDescription = desc;
    }

}
