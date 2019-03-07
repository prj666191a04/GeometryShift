using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalTutorialEnemySpawner : SurvivalLevel1EnemySpawner
{

    // Start is called before the first frame update
    void Start()
    {
        LoadEnemiesFromConglomerate();
        SetupEnemyDefaultVariables();


        timeToPhase = new Hashtable();//unique for each level

        //use: timeToPhase.Add(secondsPassed, phaseNumber);
        //starts at phase 1, so having timeToPhase.Add(0, 1) is unnessecary

        int testPhase = 0;
        bool fastMode = false;//if true, phases change faster than normal
        if (testPhase == 0)
        {
            if (fastMode)
            {
                timeToPhase.Add(1, 1);//slow projectiles
                timeToPhase.Add(3, 2);//slow + fast projectiles
                timeToPhase.Add(6, 3);//homing missiles
                timeToPhase.Add(12, 4);//boomerangs
                timeToPhase.Add(15, 5);//small waves from left and right
                timeToPhase.Add(19, 6);//small waves from left and right


                timeToPhase.Add(25, -1);//win
            }
            else
            {
                timeToPhase.Add(1, 1);//slow projectiles
                timeToPhase.Add(10, 2);//slow + fast projectiles
                timeToPhase.Add(20, 3);//homing missiles
                timeToPhase.Add(30, 4);//boomerangs
                timeToPhase.Add(40, 5);//small waves from left and right
                timeToPhase.Add(50, 6);//fast projectiles


                timeToPhase.Add(60, -1);//win

            }
        }
        else
        {
            phase = testPhase;
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
                    //slow projectiles
                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    Instantiate(slowEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
                    //a.gameObject.transform.Translate(7f, 0f, 7f);
                }
                break;
            case 2:

                cooldown1 = 0.8f;
                cooldown1TimeCounter += enemySpawnFunctionCallInterval;

                if (cooldown1TimeCounter > cooldown1)
                {
                    cooldown1TimeCounter -= cooldown1;
                    //slow projectiles

                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    Instantiate(slowEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
                }

                cooldown2 = 1f;
                cooldown2TimeCounter += enemySpawnFunctionCallInterval;

                if (cooldown2TimeCounter > cooldown2)
                {
                    //fast projectiles
                    cooldown2TimeCounter -= cooldown2;

                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    spawnRotation = Quaternion.Euler(0f, Random.Range(-30f, 30f), 0f);
                    Instantiate(fastEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
                }
                break;
            case 3:
                cooldown1 = 1f;
                cooldown1TimeCounter += enemySpawnFunctionCallInterval;
                if (cooldown1TimeCounter > cooldown1)
                {
                    cooldown1TimeCounter -= cooldown1;
                    //homing missiles

                    homingMissileScript.fuelTime = 1.5f;
                    homingMissileScript.turnSpeed = 0.6f;

                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    spawnRotation = Quaternion.Euler(0f, Random.Range(-40f, 40f), 0f);
                    Instantiate(homingMissile, spawnPosition, spawnRotation, transform.parent);

                }
                break;
            case 4:
                cooldown1 = 1.3f;
                cooldown1TimeCounter += enemySpawnFunctionCallInterval;
                if (cooldown1TimeCounter > cooldown1)
                {
                    cooldown1TimeCounter -= cooldown1;
                    //homing missiles

                    boomerangScript.accelerationPerSecond = 4f;

                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    Instantiate(boomerang, spawnPosition, spawnRotation, transform.parent);

                }
                break;
            case 5:
                cooldown1 = 1.2f;
                cooldown1TimeCounter += enemySpawnFunctionCallInterval;
                if (cooldown1TimeCounter > cooldown1)
                {
                    cooldown1TimeCounter -= cooldown1;
                    spawnWave(slowEnemyProjectile, 2, 1);
                    spawnWave(slowEnemyProjectile, 4, 1);

                }
                break;
            case 6:
                cooldown2 = 0.4f;
                cooldown2TimeCounter += enemySpawnFunctionCallInterval;
                if (cooldown2TimeCounter > cooldown2)
                {
                    //fast projectiles
                    cooldown2TimeCounter -= cooldown2;

                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    spawnRotation = Quaternion.Euler(0f, Random.Range(-30f, 30f), 0f);
                    Instantiate(fastEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
                }
                break;
            case -1:

                //Win level
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
            setPhase();
            WhatEnemiesShouldSpawn();
        }
    }
}
