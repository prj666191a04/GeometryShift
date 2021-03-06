﻿//Author Atilla puskas
//Description: a data structure used for mataining presistant game data

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class DataCore 
{
    public static int levelCount = 15;
    public static int maxItemStack = 99;

    [SerializeField]
    public GroupedData groupedData;
    //other non save file related data if any

    public DataCore(GroupedData g)
    {
        groupedData = g;
    }
    public DataCore()
    {
        groupedData = null;
    }

}
[System.Serializable]
public class GroupedData
{
    [SerializeField]
    public int slot;
    [SerializeField]
    public PlayerData playerData;
    [SerializeField]
    public WorldState worldState;

    public GroupedData(PlayerData d, WorldState w, int s)
    {
        playerData = d;
        worldState = w;
        slot = s;
    }

}
[System.Serializable]
public class PlayerData
{
    [SerializeField]
    public string name;
    //In secconds
    [SerializeField]
    public long playTime;

    [SerializeField]
    float posX;
    [SerializeField]
    float posY;
    [SerializeField]
    float posZ;

    [SerializeField]
    public GSInventory inventory_;

    //TODO: Inventory info
    public PlayerData()
    {
        name = "";
        playTime = 0;
        SetPosition(Vector3.zero);
    }

    public PlayerData(string n, long t, Vector3 pos, GSInventory inventory)
    {
        name = n;
        playTime = t;
        SetPosition(pos);
        inventory = inventory_;
    }
    public void SetPosition(Vector3 pos)
    {
        posX = pos.x;
        posY = pos.y;
        posZ = pos.z;
    }
    public Vector3 GetPosition()
    {
        return new Vector3(posX, posY, posZ);
    }


}
[System.Serializable]
public class WorldState
{
    [SerializeField]
    public Leveldata[] levelState;
    public WorldState(Leveldata[] state)
    {
        levelState = state;
    }
    public void ComfirmArraySize()
    {
        if (levelState.Length != DataCore.levelCount)
        {
            Array.Resize(ref levelState, DataCore.levelCount);

            for(int i = 0; i < levelState.Length; i++)
            {
                if(levelState[i] != null)
                {
                    levelState[i] = new Leveldata(i);
                }
            }
        }
    }
}
[System.Serializable]
public class Leveldata {
    [SerializeField]
    private int levelId;
    [SerializeField]
    private int compleeteCode;
    [SerializeField]
    private int timesCompleeted;

    public int GetCompleetedCode()
    {
        return compleeteCode;
    }

    public int GetTimesCompleeted()
    {
        return timesCompleeted;
    }

    public int GetId()
    {
        return levelId;
    }
    public void SetEmptyState(int id)
    {
        levelId = id;
        compleeteCode = -1;
        timesCompleeted = 0;
    }
    public Leveldata(int id)
    {
        levelId = id;
        compleeteCode = -1;
    }
    public Leveldata(int id, int code)
    {
        levelId = id;
        compleeteCode = code;
    }
    public void Update(int code)
    {
        compleeteCode = code;
        timesCompleeted += 1;
    }

}


[System.Serializable]
public class GSInventory
{
    [SerializeField]
    public SavedItem recoverys_;
    public GSInventory(SavedItem recoverys)
    {
        recoverys_ = recoverys;
    }

    public void Check()
    {
        if (recoverys_ == null)
        {
            recoverys_ = new SavedItem(0, 0);
        }
    }
    
}

[System.Serializable]
public class SavedItem
{
    [SerializeField]
    public int id_;
    [SerializeField]
    public int qty_;

    public SavedItem(int id, int qty)
    { 
        id_ = id;
        qty_ = qty;
    }
    public void Consume(int ammount)
    {
        qty_ -= ammount;
        if(qty_ < 0)
        {
            qty_ = 0;
        }
    }
    public void Add(int ammount)
    {
        qty_ += ammount;
        if(qty_ > DataCore.maxItemStack)
        {
            qty_ = DataCore.maxItemStack;
        }

    }
}

[SerializeField]
public class UniqueItem
{
    [SerializeField]
    public int id_;
    [SerializeField]
    public bool aquired;
}









