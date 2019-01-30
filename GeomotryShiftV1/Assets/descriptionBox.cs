using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class descriptionBox : MonoBehaviour
{

    //Description box variables
    //
    //public Transform descriptionPanel; // UI Panel - used to show and hide the UI
    //public SimpleObjectPool descriptionObjectPool; // Actual description object  with info and features

    public Button button; // USE 
    public Text itemName; // Name
    public Text itemDesc; // Long description text
    public Text quantity; // Total amount of item

    private TestItem item;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(placeHold);
    }

    public void setup(TestItem currentItem)
    {
        item = currentItem;
        itemName.text = item.itemName;
        quantity.text = item.value.ToString();
        itemDesc.text = "test desc test desc test desc test desc test desc test desc test desc test desc" +
            " test desc test desc test desc test desc test desc test desc test desc test desc test desc test desc" +
            " test desc test desc test desc test desc test desc test desc test desc test desc test desc test desc";

        
    }

    void placeHold()
    {

    }

}
