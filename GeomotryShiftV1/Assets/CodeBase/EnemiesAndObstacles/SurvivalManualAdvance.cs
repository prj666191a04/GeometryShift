using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalManualAdvance : SurvivalLevel1EnemySpawner
{
    public GameObject advanceKey;
    public static int keysRemaining;
    // Start is called before the first frame update
    void Start()
    {
        keysRemaining = 2;
        phase = 0;
        LoadEnemiesFromConglomerate();
        SetupEnemyDefaultVariables();
        SetupThePlayerVariable();
        thePlayer.AddComponent<Simple3DMovement>();
        PhaseAdvanceItem.gotCollected += ShouldAdvancePhase;

        ManualAdvancePhase();
    }

    void ShouldAdvancePhase()
    {
        if (keysRemaining == 0)
        {
            ManualAdvancePhase();
        }
    }

    void ManualAdvancePhase()
    {
        phase++;
        Vector3 spawnPosition = new Vector3();
        Quaternion spawnRotation = new Quaternion();
        switch (phase)
        {
            case 1: //setup phase 1
                spawnPosition = new Vector3(3f, 0, 3f);
                Instantiate(advanceKey, spawnPosition, spawnRotation, transform.parent);
                spawnPosition = new Vector3(-3f, 0, -3f);
                Instantiate(advanceKey, spawnPosition, spawnRotation, transform.parent);
                break;
            case 2: //phase 1 complete, setup phase 1

                spawnPosition = new Vector3(3f, 0, 0);
                Instantiate(advanceKey, spawnPosition, spawnRotation, transform.parent);
                spawnPosition = new Vector3(0, 0, 3f);
                Instantiate(advanceKey, spawnPosition, spawnRotation, transform.parent);
                spawnPosition = new Vector3(-3f, 0, 0);
                Instantiate(advanceKey, spawnPosition, spawnRotation, transform.parent);
                spawnPosition = new Vector3(0, 0, -3f);
                Instantiate(advanceKey, spawnPosition, spawnRotation, transform.parent);

                break;
            default:
                print("manual advance phase has reached undefined phase: " + phase);
                break;
        }


    }

    void WhatEnemiesShouldSpawn()//60 times a second, no matter the FPS
    {
        switch (phase)
        {
            case 1:
                cooldown1 = 0.5f;
                cooldown1TimeCounter += enemySpawnFunctionCallInterval;
                if (cooldown1TimeCounter > cooldown1)
                {
                    cooldown1TimeCounter -= cooldown1;

                    spawnWave(slowEnemyProjectile, 3, 0);
                    spawnWave(slowEnemyProjectile, 1, 0);

                }
                break;
            case 2:
                cooldown1 = 0.5f;
                cooldown1TimeCounter += enemySpawnFunctionCallInterval;
                if (cooldown1TimeCounter > cooldown1)
                {
                    cooldown1TimeCounter -= cooldown1;
                    spawnWave(fastEnemyProjectile, 2, 0);
                    spawnWave(fastEnemyProjectile, 4, 0);
                }
                break;

            case -1:

                //Win level
                PhaseAdvanceItem.gotCollected -= ShouldAdvancePhase;
                LevelBase.instance.AcknowledgeLevelCompletion();

                break;
            default:
                print("phase error. phase is " + phase);
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        secondsPassed += Time.deltaTime;
        secondsPassedInt = (int)secondsPassed;
        enemySpawnTimer += Time.deltaTime;

        while (enemySpawnTimer > enemySpawnFunctionCallInterval) // to make enemy spawn function run 60 times per second
                                                                 //even when FPS is above or below 60
        {
            enemySpawnTimer -= enemySpawnFunctionCallInterval;
            WhatEnemiesShouldSpawn();
        }
    }
}
