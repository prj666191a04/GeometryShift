//Author Atilla Puskas
//Desc Blocks a section of the map if specified levelID is not compleeted

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBlockage : MonoBehaviour
{
    public int levelId;
    bool compleete = false;
    // Start is called before the first frame update
    void Start()
    {
        if(LevelLoader.instance.GetDataCore().groupedData.worldState.levelState[levelId].GetCompleetedCode() >= 0)
        {
            Debug.Log(LevelLoader.instance.GetDataCore().groupedData.worldState.levelState[levelId].GetCompleetedCode());
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
