//Author Atilla Puskas
//Desc Controls a room that traps the user for a short duration

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRoomA : MonoBehaviour
{
    public delegate void TrapRoomDel(int id);
    public static event TrapRoomDel OnTrapCleared;

    public List<GameObject> obsticlePrefabs;
    public List<GameObject> blockage;
    public List<Transform> spawnPositions;
    public Coroutine spawnRoutine;
    bool active = false;

    public float spawnInterval;
    public float waveInterval;
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
            b.SetActive(true);
        }
    }
    public IEnumerator SpawnRoutine()
    {
        for(int i = 0; i < obsticlePrefabs.Count; i++)
        {
            for (int s = 0; s < spawnAmmountPerWave; s++)
            {
                for (int t = 0; t < spawnPositions.Count; t++)
                {
                    GameObject.Instantiate(obsticlePrefabs[i], spawnPositions[t].position, spawnPositions[t].rotation, LevelBase.instance.init_.parentObject.transform);
                    yield return new WaitForSeconds(spawnInterval);
                }
            }
            yield return new WaitForSeconds(waveInterval);
        }
        yield break;
    }

}
