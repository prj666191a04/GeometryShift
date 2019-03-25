//Author Atilla puskas
//description: Opens up a map path on a specific trigger.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRoomA : MonoBehaviour
{
    public List<GameObject> blockages;

    public List<RoomKey> keys;


    private void OnEnable()
    {
        LevelBase.OnLevelReset += ResetRoom;
    }
    private void OnDisable()
    {
        LevelBase.OnLevelReset -= ResetRoom;
    }
    private void Start()
    {
        Setup();
    }

    public void Setup()
    {
        foreach (RoomKey key in keys)
        {
            key.room = this;
        }
    }

    public void UpdateStatus()
    {
        int counter = 0;
        foreach (RoomKey key in keys)
        {
            if(key.collected)
            {
                counter++;
            }
            else
            {
                return;
            }
        }
        if (counter == keys.Count)
        {
            RemoveBlock();
        }
    }
    void RemoveBlock()
    {
        foreach(GameObject b in blockages)
        {
            b.SetActive(false);
        }
    }
    void AddBlock()
    {
        foreach(GameObject b in blockages)
        {
            b.SetActive(true);
        }
    }
    void ResetRoom()
    {
        AddBlock();
        foreach(RoomKey key in keys)
        {
            key.ResetKey();
        }
    }
}
