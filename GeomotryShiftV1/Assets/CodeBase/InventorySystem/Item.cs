using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : SuperItem
{

    public Item(): base() { }// calls parent defaul constuctor Makes a blank empty item

    public Item(string name, string desc): base(name, desc){} // Used constructor to instantiate items values

    virtual public void useItem(){} // virtual  becasue Item class will be used for subclass items

}
