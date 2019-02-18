using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item2
{
    public string itemName;
    public Sprite icon;
    public float price = 1f;

}


public class Inventory2 : MonoBehaviour
{

    public List<Item2> itemList;
    public Transform contentPanel;
    public Inventory2 otherInv;
    public Text myGoldDisplay;
    public SimpleObjectPool buttonObjectPool;
    public float gold = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void AddButtons()
    {

    }
}
