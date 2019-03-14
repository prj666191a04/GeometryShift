using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalHungry : SurvivalLevel1EnemySpawner
{
    public GameObject theFood;
    Hunger hungerScript;
    SurvivalFood foodScript;
    int x = 0;
    // Start is called before the first frame update
    void Start()
    {
        LoadEnemiesFromConglomerate();
        SetupEnemyDefaultVariables();

        foodScript = theFood.gameObject.GetComponent<SurvivalFood>();

        SetupThePlayerVariable();
        thePlayer.AddComponent<Hunger>();
        thePlayer.AddComponent<Simple3DMovement>();
        hungerScript = thePlayer.gameObject.GetComponent<Hunger>();

        timeToPhase = new Hashtable();//unique for each level

        //use: timeToPhase.Add(secondsPassed, phaseNumber);
        //starts at phase 1, so having timeToPhase.Add(0, 1) is unnessecary

        int testPhase = 0;
        bool fastMode = true;//if true, phases change faster than normal
        if (testPhase == 0)
        {
            if (fastMode)
            {
                timeToPhase.Add(1, 1);//slow projectiles
                timeToPhase.Add(3, 2);//slow + fast projectiles
                timeToPhase.Add(6, 3);
                timeToPhase.Add(12, 4);
                timeToPhase.Add(17, 5);
                timeToPhase.Add(25, 6);


                timeToPhase.Add(252, -1);//win
            }
            else
            {
                timeToPhase.Add(1, 1);//slow projectiles
                timeToPhase.Add(10, 2);//slow + fast projectiles
                timeToPhase.Add(20, 3);//homing missiles
                timeToPhase.Add(30, 4);//boomerangs
                timeToPhase.Add(40, 5);//bullet sharks
                timeToPhase.Add(50, 6);//boomerang planar explosion


                timeToPhase.Add(60, -1);//win
            }
        }
        else
        {
            phase = testPhase;
        }
        SpawnNewFood();
    }

    void SpawnNewFood()
    {
        foodScript.SpawnNewFood(transform.parent);
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

                cooldown1 = 0.8f;
                cooldown1TimeCounter += enemySpawnFunctionCallInterval;

                if (cooldown1TimeCounter > cooldown1)
                {
                    cooldown1TimeCounter -= cooldown1;
                    //slow projectiles

                    spawnWave(slowEnemyProjectile, 3, 0);
                    spawnWave(slowEnemyProjectile, 1, 0);
                    spawnWave(slowEnemyProjectile, 3, 0);
                    spawnWave(slowEnemyProjectile, 1, 0);
                }

                cooldown2 = 1f;
                cooldown2TimeCounter += enemySpawnFunctionCallInterval;

                if (cooldown2TimeCounter > cooldown2)
                {
                    cooldown2TimeCounter -= cooldown2;
                    spawnWave(fastEnemyProjectile, 2, 0);
                    spawnWave(fastEnemyProjectile, 4, 0);
                }
                break;
            case 3:
                cooldown1 = 0.4f;
                cooldown1TimeCounter += enemySpawnFunctionCallInterval;
                if (cooldown1TimeCounter > cooldown1)
                {
                    cooldown1TimeCounter -= cooldown1;
                    homingMissileScript.fuelTime = 1.5f;
                    homingMissileScript.turnSpeed = 0.6f;

                    spawnWave(homingMissile, Random.Range(1, 5), 0);

                }
                break;
            case 4:
                cooldown1 = 1f;
                cooldown1TimeCounter += enemySpawnFunctionCallInterval;
                if (cooldown1TimeCounter > cooldown1)
                {
                    cooldown1TimeCounter -= cooldown1;

                    boomerangScript.accelerationPerSecond = 4f;

                    boomerangScript.speed = 12f;

                    spawnWave(boomerang, Random.Range(1, 5), 0);
                    

                }
                break;
            case 5:
                cooldown1 = 0.2f;
                cooldown1TimeCounter += enemySpawnFunctionCallInterval;
                if (cooldown1TimeCounter > cooldown1)
                {
                    bulletSharkScript.whatToShoot = slowEnemyProjectile;
                    bulletSharkScript.shootInterval = 0.8f;
                    bulletSharkScript.turnSpeed = 0f;
                    bulletSharkScript.numberOfProjectilesToShoot = 4;
                    cooldown1TimeCounter -= cooldown1;
                    spawnWave(bulletShark, Random.Range(1, 5), 0);

                }
                break;
            case 6:
                cooldown2 = 1.6f;
                cooldown2TimeCounter += enemySpawnFunctionCallInterval;
                if (cooldown2TimeCounter > cooldown2)
                {
                    cooldown2TimeCounter -= cooldown2;
                    boomerangScript.accelerationPerSecond = 6f;

                    boomerangScript.speed = 6f;
                    spawnPlanarExplosion(boomerang, 5);
                    x++;
                    print(x);

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
