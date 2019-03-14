using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalManualAdvance : SurvivalLevel1EnemySpawner
{
    public GameObject advanceKey;
    public static int keysRemaining;
    float spawnLocation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        keysRemaining = 0;
        phase = 0;
        LoadEnemiesFromConglomerate();
        SetupEnemyDefaultVariables();
        SetupThePlayerVariable();
        thePlayer.AddComponent<Simple3DMovement>();
        PhaseAdvanceItem.gotCollected += ShouldAdvancePhase;

        ManualAdvancePhase();
    }

    void SpawnKey(float x, float z)
    {
        Vector3 spawnPosition = new Vector3(x, 0f, z);
        Instantiate(advanceKey, spawnPosition, new Quaternion(), GeometryShift.playerStatus.gameObject.transform.parent);
        keysRemaining++;
    }

    void ShouldAdvancePhase()
    {
        keysRemaining--;
        print("keys remain " + keysRemaining + " current phase " + phase);
        if (keysRemaining <= 0)
        {
            ManualAdvancePhase();
        }
    }

    public static void x()
    {
        
    }

    void ManualAdvancePhase()
    {
        keysRemaining = 0;
        phase++;
        print("adv phase to " + phase);
        switch (phase)
        {
            case 1: //setup phase 1
                SpawnKey(3f, 3f);
                SpawnKey(-3f, -3f);
                break;

            case 2: //phase 1 complete, setup phase 2
                SpawnKey(3f, 0);
                SpawnKey(-3f, 0);
                SpawnKey(0, 3f);
                SpawnKey(0, -3f);
                break;

            case 3:
                SpawnKey(5f, 0);
                SpawnKey(-5f, 0);
                SpawnKey(0, 5f);
                SpawnKey(0, -5f);
                break;

            case 4:
                SpawnKey(3f, 0);
                SpawnKey(-3f, 0);
                SpawnKey(0, 3f);
                SpawnKey(0, -3f);

                SpawnKey(5f, 0);
                SpawnKey(-5f, 0);
                SpawnKey(0, 5f);
                SpawnKey(0, -5f);
                break;

            case 5:
                for (int i = 0; i < 7; i++)
                {
                    for (int i2 = 0; i2 < 7; i2++)
                    {
                        SpawnKey((float)((i - 3) * 1.8), (float)((i2 - 3) * 1.8));
                    }
                }
                break;
            case 6:
                phase = -1;
                //Win level
                
                LevelBase.instance.AcknowledgeLevelCompletion();
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

            case 3:
                spawnLocation += 1;
                cooldown1 = 0.3f;
                cooldown1TimeCounter += enemySpawnFunctionCallInterval;
                if (cooldown1TimeCounter > cooldown1)
                {
                    cooldown1TimeCounter -= cooldown1;
                    spawnWave(slowEnemyProjectile, 3, 0, 0, 0, 0, spawnLocation);
                }
                break;

            case 4:
                spawnLocation += 1;
                cooldown1 = 0.3f;
                cooldown1TimeCounter += enemySpawnFunctionCallInterval;
                if (cooldown1TimeCounter > cooldown1)
                {
                    cooldown1TimeCounter -= cooldown1;
                    spawnWave(fastEnemyProjectile, 3, 0, 0, 0, 0, spawnLocation);
                }
                break;

            case 5:
                spawnLocation += 1;
                cooldown1 = 0.5f;
                cooldown1TimeCounter += enemySpawnFunctionCallInterval;
                if (cooldown1TimeCounter > cooldown1)
                {
                    cooldown1TimeCounter -= cooldown1;
                    spawnWave(homingMissile, 3, 0, 0, 0, 0, spawnLocation);
                }
                break;

            case -1:

                

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

    private void OnDestroy()
    {
        PhaseAdvanceItem.gotCollected -= ShouldAdvancePhase;
    }
}
