//Author Atilla Puskas
//Desc Controls a room that traps the user for a short duration

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRoomA : MonoBehaviour
{
    public int roomID;
    public delegate void TrapRoomDel(int id);
    public static event TrapRoomDel OnTrapCleared;

    public List<GameObject> blockage;
    public List<SpawnGroup> spawnGroups;


    public Coroutine spawnRoutine;
    bool active = false;

    public int spawnAmmountPerWave;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void OnEnable()
    {
        CStatus.OnPlayerDeath += CancleSpawnRoutine;
        LevelBase.OnLevelReset += ResetTrap;
    }
    private void OnDisable()
    {
        CStatus.OnPlayerDeath -= CancleSpawnRoutine;
        LevelBase.OnLevelReset -= ResetTrap;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !active)
        {
            active = true;
            spawnRoutine = StartCoroutine(SpawnRoutine());
        }
    }
    void ResetTrap()
    {
        active = false;
        RemoveBlockage();
    }

    void CancleSpawnRoutine(int m = 0)
    {
        if(spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
        }
    }
    public void AddBlockage()
    {
        foreach(GameObject b in blockage)
        {
            b.SetActive(true);
        }
    }
    public void RemoveBlockage()
    {
        foreach(GameObject b in blockage)
        {
            b.SetActive(false);
        }
    }
    public IEnumerator SpawnRoutine()
    {
        AddBlockage();
        for(int i = 0; i < spawnGroups.Count; i++)
        {
            for (int s = 0; s < spawnGroups[i].waveAmmount; s++)
            {
                for (int t = 0; t < spawnGroups[i].spawms.Count; t++)
                {
                    GameObject.Instantiate(spawnGroups[i].prefab, spawnGroups[i].spawms[t].position, spawnGroups[i].spawms[t].rotation, LevelBase.instance.init_.parentObject.transform);
                    yield return new WaitForSeconds(spawnGroups[i].spawnInterval);
                }
            }
            yield return new WaitForSeconds(spawnGroups[i].nextWaveDelay);
        }
        Debug.Log("trapEnd");
        RemoveBlockage();
        if(OnTrapCleared != null)
        {
            OnTrapCleared(roomID);
        }

        yield break;
      
    }

}



[System.Serializable]
public class SpawnGroup
{
    [SerializeField]
    public List<Transform> spawms;

    [SerializeField]
    public int waveAmmount;

    [SerializeField]
    public float nextWaveDelay;

    [SerializeField]
    public float spawnInterval;

    [SerializeField]
    public GameObject prefab;
}