using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalLevel1EnemySpawner : MonoBehaviour
{
    float widthOfLevel = 22f;
    float lengthOfLevel = 22f;

    float secondsSinceLastPlanarExplosion = 0;
    float secondsBetweenEachPlanarExplosion = 0.8f;

    float secondsPassed = 0;
    int secondsPassedInt = 0;
    float enemySpawnTimer = 0;
    Random random = new Random();


    GameObject fastEnemyProjectile;
    GameObject slowEnemyProjectile;
    GameObject homingMissile;
    GameObject bulletShark;
    GameObject planarExplosion;

    Hashtable timeToPhase;
    int phase = 1;

    // Start is called before the first frame update
    void Start()
    {

        fastEnemyProjectile = Resources.Load("Enemies/EnemyTurretFolder/Enemy Projectile") as GameObject;
        fastEnemyProjectile.gameObject.GetComponent<EnemyProjectile>().maximumLifespanAllowed = 10;
        fastEnemyProjectile.gameObject.GetComponent<EnemyProjectile>().speed = 10f;

        slowEnemyProjectile = Resources.Load("Enemies/EnemyTurretFolder/Enemy Projectile Slow") as GameObject;
        slowEnemyProjectile.gameObject.GetComponent<EnemyProjectile>().maximumLifespanAllowed = 12;
        slowEnemyProjectile.gameObject.GetComponent<EnemyProjectile>().speed = 6f;

        homingMissile = Resources.Load("Enemies/HomingMissile/Enemy Homing Missile") as GameObject;
        homingMissile.gameObject.GetComponent<EnemyHomingMissile>().maximumLifespanAllowed = 12;


        planarExplosion = Resources.Load("Enemies/PlanarExplosion/PlanarExplosion") as GameObject;


        bulletShark = Resources.Load("Enemies/BulletShark/Enemy Bullet Shark") as GameObject;
        bulletShark.gameObject.GetComponent<EnemyBulletShark>().maximumLifespanAllowed = 12;


        timeToPhase = new Hashtable();

        //use: timeToPhase.Add(secondsPassed, phaseNumber);
        //starts at phase 1, so having timeToPhase.Add(0, 1) is unnessecary

        int testPhase = 0;
        bool fastMode = false;//if true, phases change faster than normal
        if (testPhase == 0)
        {
            if (fastMode)
            {
                timeToPhase.Add(1, 1);//slow projectiles
                timeToPhase.Add(5, 2);//slow + fast projectiles
                timeToPhase.Add(10, 3);//planar explosions + fast projectiles
                timeToPhase.Add(15, 4);//homing missiles
                timeToPhase.Add(20, 5);//planar explosions that spawn homing missiles
                timeToPhase.Add(25, 0);//break time
                timeToPhase.Add(27, 6);//projectile waves from top and bottom
                timeToPhase.Add(35, 7);//double layer planar explosions: fast and slow projectiles
                timeToPhase.Add(40, 8);//double layer planar explosions: slow projectiles and homing missiles
                timeToPhase.Add(47, 0);//break time
                timeToPhase.Add(50, 9);//fast projectiles from all directions
                timeToPhase.Add(58, 10);//slow projectiles from all directions, up to 45 degree angle variation
                timeToPhase.Add(65, 11);//spawn planar explosions on edge of map only
                timeToPhase.Add(71, 12);//bullet sharks from bottom
                timeToPhase.Add(83, 13);//bullet sharks from sides
                timeToPhase.Add(100, -1);//win
            }
            else
            {
                timeToPhase.Add(1, 1);//slow projectiles
                timeToPhase.Add(15, 2);//slow + fast projectiles
                timeToPhase.Add(35, 3);//planar explosions + fast projectiles
                timeToPhase.Add(55, 4);//homing missiles
                timeToPhase.Add(75, 5);//planar explosions that spawn homing missiles
                timeToPhase.Add(90, 0);//break time
                timeToPhase.Add(110, 6);//projectile waves from top and bottom
                timeToPhase.Add(130, 7);//double layer planar explosions: fast and slow projectiles
                timeToPhase.Add(150, 8);//double layer planar explosions: slow projectiles and homing missiles
                timeToPhase.Add(170, 9);//fast projectiles from all directions
                timeToPhase.Add(190, 10);//slow projectiles from all directions, up to 45 degree angle variation
                timeToPhase.Add(220, 11);//spawn planar explosions on edge of map only
                timeToPhase.Add(240, 12);//bullet sharks from bottom
                timeToPhase.Add(270, 13);//bullet sharks from sides
                timeToPhase.Add(300, -1);//win
            }
        }
        else
        {
            phase = testPhase;
        }

    }

    void setPhase()
    {
        if (timeToPhase.ContainsKey(secondsPassedInt))
        {
            phase = (int)timeToPhase[secondsPassedInt];
            timeToPhase.Remove(secondsPassedInt);
            secondsSinceLastPlanarExplosion = 0;
        }
    }

    void spawnPlanarExplosion(GameObject projectile,
        int number = 6,
        bool randomSpawnLocation = true,
        float fuseTime = 1f,
        float x = 0,
        float z = 0
        )
    {
        Vector3 spawnPosition;
        if (randomSpawnLocation)
        {
            spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, Random.Range(-(lengthOfLevel / 2), (lengthOfLevel / 2)));
        }
        else
        {
            spawnPosition = new Vector3(x, 0f, z);
        }
        Quaternion spawnRotation = new Quaternion();
        planarExplosion.GetComponent<PlanarExplosion>().theEnemyToSpawn = projectile;
        planarExplosion.GetComponent<PlanarExplosion>().numberOfEnemiesSpawned = number;
        planarExplosion.GetComponent<PlanarExplosion>().maximumLifespanAllowed = fuseTime;
        Instantiate(planarExplosion, spawnPosition, spawnRotation, transform.parent);
    }

    void spawnWave(GameObject projectile,
        int side = 3, //1 = top, 2 = right, 3 = bottom, 4 = left
        int numberOfFlankingProjectilesOnEachSide = 2,
        float widthSpacing = 0.6f,
        float distanceSpacing = 0.2f,
        float angleVariation = 0f
        )
    {
        float actualAngleVariation = Random.Range(-angleVariation, angleVariation);

        float x = 0;
        float y = 0;

        Vector3 spawnPosition;
        Quaternion spawnRotation;

        switch (side)
        {
            case 1:
                x = Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2));
                y = (lengthOfLevel / 2);

                spawnPosition = new Vector3(x, 0f, y);
                spawnRotation = Quaternion.Euler(0f, 180f + actualAngleVariation, 0f);

                Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);

                for (int i = 1; i <= numberOfFlankingProjectilesOnEachSide; i++)
                {
                    spawnPosition = new Vector3(x + (i * widthSpacing), 0f, y + (i * distanceSpacing));
                    Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);
                    spawnPosition = new Vector3(x - (i * widthSpacing), 0f, y + (i * distanceSpacing));
                    Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);
                }
                break;
            case 2:
                x = (lengthOfLevel / 2);
                y = Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2));

                spawnPosition = new Vector3(x, 0f, y);
                spawnRotation = Quaternion.Euler(0f, 270f + actualAngleVariation, 0f);

                Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);

                for (int i = 1; i <= numberOfFlankingProjectilesOnEachSide; i++)
                {

                    spawnPosition = new Vector3(x + (i * distanceSpacing), 0f, y + (i * widthSpacing));
                    Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);

                    spawnPosition = new Vector3(x + (i * distanceSpacing), 0f, y - (i * widthSpacing));
                    Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);
                }
                break;
            case 3:
                x = Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2));
                y = -(lengthOfLevel / 2);

                spawnPosition = new Vector3(x, 0f, y);
                spawnRotation = Quaternion.Euler(0f, actualAngleVariation, 0f);

                Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);

                for (int i = 1; i <= numberOfFlankingProjectilesOnEachSide; i++)
                {
                    spawnPosition = new Vector3(x + (i * widthSpacing), 0f, y - (i * distanceSpacing));
                    Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);
                    spawnPosition = new Vector3(x - (i * widthSpacing), 0f, y - (i * distanceSpacing));
                    Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);
                }
                break;
            case 4:
                x = -(lengthOfLevel / 2);
                y = Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2));

                spawnPosition = new Vector3(x, 0f, y);
                spawnRotation = Quaternion.Euler(0f, 90f + actualAngleVariation, 0f);

                Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);

                for (int i = 1; i <= numberOfFlankingProjectilesOnEachSide; i++)
                {
                    spawnPosition = new Vector3(x - (i * distanceSpacing), 0f, y + (i * widthSpacing));
                    Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);


                    spawnPosition = new Vector3(x - (i * distanceSpacing), 0f, y - (i * widthSpacing));
                    Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);
                }
                break;
            default:
                print("spawnWave side needs to be 1-4. It recieved " + side);
                break;
        }




    }

    void WhatEnemiesShouldSpawn()//60 times a second, no matter the FPS
    {
        switch (phase)
        {
            case 1:
                if (Random.Range(0f, 60f) <= 2)
                {
                    //slow projectiles
                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    Instantiate(slowEnemyProjectile, spawnPosition, spawnRotation, transform.parent);

                }
                break;
            case 2:
                if (Random.Range(0f, 60f) <= 3)
                {
                    //slow projectiles

                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    Instantiate(slowEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
                }
                if (Random.Range(0f, 60f) <= 2)
                {
                    //fast projectiles

                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    spawnRotation = Quaternion.Euler(0f, Random.Range(-30f, 30f), 0f);
                    Instantiate(fastEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
                }
                break;
            case 3:
                secondsSinceLastPlanarExplosion += Time.deltaTime;
                secondsBetweenEachPlanarExplosion = 0.6f;
                if (secondsSinceLastPlanarExplosion > secondsBetweenEachPlanarExplosion)
                {
                    //planar explosions that spawn slow enemy projectiles
                    secondsSinceLastPlanarExplosion -= secondsBetweenEachPlanarExplosion;
                    spawnPlanarExplosion(slowEnemyProjectile, 10, true, 1f);
                }
                break;
            case 4:
                if (Random.Range(0f, 60f) <= 3)
                {
                    //homing missiles

                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    spawnRotation = Quaternion.Euler(0f, Random.Range(-40f, 40f), 0f);
                    Instantiate(homingMissile, spawnPosition, spawnRotation, transform.parent);

                }
                break;
            case 5:
                secondsBetweenEachPlanarExplosion = 1.2f;

                secondsSinceLastPlanarExplosion += Time.deltaTime;
                if (secondsSinceLastPlanarExplosion > secondsBetweenEachPlanarExplosion)
                {
                    //planar explosion that spawns homing missiles
                    secondsSinceLastPlanarExplosion -= secondsBetweenEachPlanarExplosion;
                    spawnPlanarExplosion(homingMissile, 5);
                }
                break;
            case 6:
                //waves top and bottom slow projectiles

                if (Random.Range(0f, 60f) <= 2.6)
                {
                    spawnWave(slowEnemyProjectile, 3);
                }

                if (Random.Range(0f, 60f) <= 2.6)
                {
                    spawnWave(slowEnemyProjectile, 1);

                }
                break;
            case 7:
                //double layer planar explosions: fast and slow projectiles
                secondsBetweenEachPlanarExplosion = 1.2f;

                secondsSinceLastPlanarExplosion += Time.deltaTime;

                if (secondsSinceLastPlanarExplosion > secondsBetweenEachPlanarExplosion)
                {
                    //planar explosion that spawns homing missiles
                    float tempX = Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2));
                    float tempY = Random.Range(-(lengthOfLevel / 2), (lengthOfLevel / 2));
                    secondsSinceLastPlanarExplosion -= secondsBetweenEachPlanarExplosion;
                    spawnPlanarExplosion(slowEnemyProjectile, 16, false, 1f, tempX, tempY);
                    spawnPlanarExplosion(fastEnemyProjectile, 12, false, 1f, tempX, tempY);
                }
                break;
            case 8:
                //double layer planar explosions: slow projectiles and homing missiles
                secondsBetweenEachPlanarExplosion = 1.6f;

                secondsSinceLastPlanarExplosion += Time.deltaTime;

                if (secondsSinceLastPlanarExplosion > secondsBetweenEachPlanarExplosion)
                {
                    float tempX = Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2));
                    float tempY = Random.Range(-(lengthOfLevel / 2), (lengthOfLevel / 2));
                    secondsSinceLastPlanarExplosion -= secondsBetweenEachPlanarExplosion;
                    homingMissile.GetComponent<EnemyHomingMissile>().fuelTime = 0.5f;
                    homingMissile.GetComponent<EnemyHomingMissile>().turnSpeed = 3.5f;
                    spawnPlanarExplosion(slowEnemyProjectile, 12, false, 1f, tempX, tempY);
                    spawnPlanarExplosion(homingMissile, 12, false, 1f, tempX, tempY);
                }
                break;
            case 9:
                //fast projectiles from all directions
                if (Random.Range(0f, 60f) <= 8)
                {
                    spawnWave(fastEnemyProjectile, Random.Range(1, 5), 0, 1f, 1f);
                }

                break;
            case 10:
                //slow projectiles from all directions, up to 45 degree angle variation
                if (Random.Range(0f, 60f) <= 16)
                {
                    spawnWave(slowEnemyProjectile, Random.Range(1, 5), 0, 1f, 1f, 45f);
                }

                break;
            case 11:
                //spawn planar explosions on edge of map only

                secondsSinceLastPlanarExplosion += Time.deltaTime;
                secondsBetweenEachPlanarExplosion = 0.4f;
                if (secondsSinceLastPlanarExplosion > secondsBetweenEachPlanarExplosion)
                {
                    secondsSinceLastPlanarExplosion -= secondsBetweenEachPlanarExplosion;
                    planarExplosion.GetComponent<PlanarExplosion>().theEnemyToSpawn = slowEnemyProjectile;
                    planarExplosion.GetComponent<PlanarExplosion>().numberOfEnemiesSpawned = 10;
                    //planarExplosion.
                    spawnWave(planarExplosion, Random.Range(1, 5), 0);
                }

                break;

            case 12:
                //bullet sharks from bottom
                secondsSinceLastPlanarExplosion += Time.deltaTime;
                secondsBetweenEachPlanarExplosion = 1f;
                if (secondsSinceLastPlanarExplosion > secondsBetweenEachPlanarExplosion)
                {
                    secondsSinceLastPlanarExplosion -= secondsBetweenEachPlanarExplosion;
                    //slow projectiles
                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    bulletShark.GetComponent<EnemyBulletShark>().whatToShoot = slowEnemyProjectile;
                    bulletShark.GetComponent<EnemyBulletShark>().shootInterval = 0.5f;
                    bulletShark.GetComponent<EnemyBulletShark>().overrideTurnSpeed = 0f;
                    bulletShark.GetComponent<EnemyBulletShark>().numberOfProjectilesToShoot = 4;
                    Instantiate(bulletShark, spawnPosition, spawnRotation, transform.parent);
                }
                break;
            case 13:
                //bullet sharks from sides
                secondsSinceLastPlanarExplosion += Time.deltaTime;
                secondsBetweenEachPlanarExplosion = 1.5f;
                if (secondsSinceLastPlanarExplosion > secondsBetweenEachPlanarExplosion)
                {
                    secondsSinceLastPlanarExplosion -= secondsBetweenEachPlanarExplosion;
                   
                    bulletShark.GetComponent<EnemyBulletShark>().whatToShoot = slowEnemyProjectile;
                    bulletShark.GetComponent<EnemyBulletShark>().shootInterval = 0.5f;
                    bulletShark.GetComponent<EnemyBulletShark>().overrideTurnSpeed = 0f;
                    bulletShark.GetComponent<EnemyBulletShark>().numberOfProjectilesToShoot = 2;
                    //Instantiate(bulletShark, spawnPosition, spawnRotation, transform.parent);
                    spawnWave(bulletShark, 2, 0);
                    spawnWave(bulletShark, 4, 0);
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

        while (enemySpawnTimer > 0.0167f) // to make enemy spawn function run 60 times per second
                                          //even when FPS is above or below 60
        {
            enemySpawnTimer -= 0.0167f;
            setPhase();
            WhatEnemiesShouldSpawn();
        }

    }
}
