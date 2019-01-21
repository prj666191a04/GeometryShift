using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//INPORTANT values ending with Prefab Should not be modified at runtime.

public class GeomotryShift : MonoBehaviour
{

    public GeomotryShift instance;

    public static SystemState systemState = SystemState.MainMenue;
    public enum SystemState
    {
        MainMenue,
        WorldMap,
        InLevel,
        Loading
    }


    //UI- this code might need to be moved to a ui manager class later on
    public GameObject mainMenuePrefab;
    public GameObject openWorldUiPrefab;
    private GameObject loadedUiSet;


    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
 

}
