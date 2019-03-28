//Author Atilla Puskas
//desc listens for a traproom to be compleeted and then reacts

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenterBlockage : MonoBehaviour
{
    public int myId_;
    public List<GameObject> blockage;
    // Start is called before the first frame update

    private void OnEnable()
    {
        TrapRoomA.OnTrapCleared += Listen;
        LevelBase.OnLevelReset += AddBlock;
    }
    private void OnDisable()
    {
        TrapRoomA.OnTrapCleared -= Listen;
        LevelBase.OnLevelReset -= AddBlock;
    }
    void Listen(int id)
    {
        if(id == myId_)
        {
            RemoveBlock();
        }
    }
    
    void AddBlock()
    {
        foreach (GameObject b in blockage)
        {
            b.SetActive(true);
        }
    }

    void RemoveBlock()
    {
        foreach (GameObject b in blockage)
        {
            b.SetActive(false);
        }
    }
}
