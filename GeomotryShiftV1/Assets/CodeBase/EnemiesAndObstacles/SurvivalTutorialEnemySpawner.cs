using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalTutorialEnemySpawner : SurvivalLevel1EnemySpawner
{
    float spawnLocationFloat = 0f;
    float spawnOrNot = 60f;
    // Start is called before the first frame update

    void InitLevelSurvivalTutorial()
    {
        InvokeRepeating("Update60TimesPerSecond", 0.0166f, 0.0166f);

        phase = 1;

        SurvivalLevelInit();

        timeToPhase = new Hashtable();//unique for each level

        //use: timeToPhase.Add(secondsPassed, phaseNumber);
        //starts at phase 1, so having timeToPhase.Add(0, 1) is unnessecary

        int testPhase = 0;
        bool fastMode = false;//if true, phases change faster than normal
        if (testPhase == 0)
        {
            if (fastMode)
            {
                timeToWin = 25;
                timeToPhase.Add(1, 1);//slow projectiles
                timeToPhase.Add(3, 2);//slow + fast projectiles
                timeToPhase.Add(6, 3);//homing missiles
                timeToPhase.Add(12, 4);//boomerangs
                timeToPhase.Add(15, 5);//small waves from left and right
                timeToPhase.Add(timeToWin, 6);//small waves from left and right


                timeToPhase.Add(25, -1);//win
            }
            else
            {
                timeToWin = 60;
                timeToPhase.Add(1, 1);//slow projectiles
                timeToPhase.Add(10, 2);//slow + fast projectiles
                timeToPhase.Add(20, 3);//homing missiles
                timeToPhase.Add(30, 4);//boomerangs
                timeToPhase.Add(40, 5);//small waves from left and right
                timeToPhase.Add(50, 6);//fast projectiles


                timeToPhase.Add(timeToWin, -1);//win

            }
        }
        else
        {
            phase = testPhase;
        }
    }

    private void OnEnable()
    {
        UniversalSurvivalOnEnable();
        LevelOverlayUI.OnIntroFinished += InitLevelSurvivalTutorial;
    }

    private void OnDisable()
    {
        UniversalSurvivalOnDisable();
        LevelOverlayUI.OnIntroFinished -= InitLevelSurvivalTutorial;
    }


    void Start()
    {
        theUI.PlayIntro();
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
                cooldown1 = 0.16f;
                cooldown1TimeCounter += enemySpawnFunctionCallInterval;

                spawnOrNot -= 1f;
                if (spawnOrNot <= -60)
                {
                    spawnOrNot = 60f;
                }

                if (cooldown1TimeCounter > cooldown1)
                {
                    cooldown1TimeCounter -= cooldown1;
                    boomerangScript.speed = 23f;
                    boomerangScript.accelerationPerSecond = 12f;

                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    //Quaternion spawnRotation = new Quaternion();
                    if (spawnOrNot > 0f)
                    {
                        spawnWave(slowEnemyProjectile, 1, 0, 0, 0, 45, spawnLocationFloat);
                    }
                    
                    spawnLocationFloat += 4.5f;
                    
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
                //LevelBase.instance.AcknowledgeLevelCompletion();
                phase = -999;
                theUI.ShowRsltScreen("You Win!" + System.Environment.NewLine + "Level Completed.", 0);
                CancelInvoke();

                break;
            default:
                print("phase error. phase is " + phase);
                break;
        }

    }

    private void Update60TimesPerSecond()
    {
        setPhase();
        WhatEnemiesShouldSpawn();

        secondsPassed += 0.0166f;
        secondsPassedInt = (int)secondsPassed;
        updateTimeRemaining();
    }
}
