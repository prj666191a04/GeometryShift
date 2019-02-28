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
    float weakProjectileScale = 0.82f;
    float strongProjectileScale = 1.3f;
    Random random = new Random();


    GameObject fastEnemyProjectile;
    GameObject slowEnemyProjectile;
    GameObject homingMissile;
    GameObject planarExplosion;

    Hashtable timeToPhase;
    int phase = 1;

    // Start is called before the first frame update
    void Start()
    {

        fastEnemyProjectile = Resources.Load("Enemies/EnemyTurretFolder/Enemy Projectile") as GameObject;
        fastEnemyProjectile.gameObject.GetComponent<EnemyProjectile>().maximumLifespanAllowed = 12;
        fastEnemyProjectile.gameObject.GetComponent<EnemyProjectile>().speed = 10f;

        slowEnemyProjectile = Resources.Load("Enemies/EnemyTurretFolder/Enemy Projectile Slow") as GameObject;
        slowEnemyProjectile.gameObject.GetComponent<EnemyProjectile>().maximumLifespanAllowed = 12;
        slowEnemyProjectile.gameObject.GetComponent<EnemyProjectile>().speed = 6f;

        homingMissile = Resources.Load("Enemies/HomingMissile/Enemy Homing Missile") as GameObject;
        homingMissile.gameObject.GetComponent<EnemyHomingMissile>().maximumLifespanAllowed = 12;


        planarExplosion = Resources.Load("Enemies/PlanarExplosion/PlanarExplosion") as GameObject;


        timeToPhase = new Hashtable();

        //use: timeToPhase.Add(secondsPassed, phaseNumber);
        //starts at phase 1, so having timeToPhase.Add(0, 1) is unnessecary

        timeToPhase.Add(1, 1);//slow projectiles
        timeToPhase.Add(5, 2);//slow + fast projectiles
        timeToPhase.Add(10, 3);//planar explosions + fast projectiles
        timeToPhase.Add(15, 4);//homing missiles
        timeToPhase.Add(20, 5);//planar explosions that spawn homing missiles
        timeToPhase.Add(25, 0);//break time
        timeToPhase.Add(27, 6);//projectile waves from top and bottom
        timeToPhase.Add(35, 7);//homing missile waves from left and right


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

    void spawnPlanarExplosion(GameObject projectile, int number = 6)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, Random.Range(-(lengthOfLevel / 2), (lengthOfLevel / 2)));
        Quaternion spawnRotation = new Quaternion();
        planarExplosion.GetComponent<PlanarExplosion>().theEnemyToSpawn = projectile;
        planarExplosion.GetComponent<PlanarExplosion>().numberOfEnemiesSpawned = number;
        Instantiate(planarExplosion, spawnPosition, spawnRotation, transform.parent);
    }

    void spawnWave(GameObject projectile,
        int side = 3, //1 = top, 2 = right, 3 = bottom, 4 = left
        int numberOfFlankingProjectilesOnEachSide = 2,
        float widthSpacing = 0.6f,
        float distanceSpacing = 0.2f)
    {

        float x = 0;
        float y = 0;

        switch (side)
        {
            case 1:
                x = Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2));
                y = (lengthOfLevel / 2);

                Vector3 spawnPosition = new Vector3(x, 0f, y);
                Quaternion spawnRotation = Quaternion.Euler(0f, 180f, 0f);

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
                spawnRotation = Quaternion.Euler(0f, 270f, 0f);

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
                spawnRotation = new Quaternion();

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
                spawnRotation = Quaternion.Euler(0f, 90f, 0f);

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

    void whatEnemiesShouldSpawn()//60 times a second, no matter the FPS
    {
        switch (phase)
        {
            case 1:
                if (Random.Range(0f, 60f) <= 5)
                {
                    //slow projectiles

                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    Instantiate(slowEnemyProjectile, spawnPosition, spawnRotation, transform.parent);


                    spawnRotation = Quaternion.Euler(0f, Random.Range(-40f, 40f), 0f);
                    Instantiate(homingMissile, spawnPosition, spawnRotation, transform.parent);

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
                if (Random.Range(0f, 60f) <= 3)
                {
                    //fast projectiles

                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    spawnRotation = Quaternion.Euler(0f, Random.Range(-40f, 40f), 0f);
                    Instantiate(fastEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
                }
                secondsSinceLastPlanarExplosion += Time.deltaTime;
                if (secondsSinceLastPlanarExplosion > secondsBetweenEachPlanarExplosion)
                {
                    //planar explosions
                    secondsSinceLastPlanarExplosion -= secondsBetweenEachPlanarExplosion;
                    spawnPlanarExplosion(slowEnemyProjectile);
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
                secondsBetweenEachPlanarExplosion = 1.5f;

                secondsSinceLastPlanarExplosion += Time.deltaTime;
                if (secondsSinceLastPlanarExplosion > secondsBetweenEachPlanarExplosion)
                {
                    //planar explosion that spawns homing missiles
                    secondsSinceLastPlanarExplosion -= secondsBetweenEachPlanarExplosion;
                    spawnPlanarExplosion(homingMissile, 4);
                }
                break;
            case 6:
                //waves

                if (Random.Range(0f, 60f) <= 3)
                {
                    spawnWave(slowEnemyProjectile, 3);
                }
                
                if (Random.Range(0f, 60f) <= 3)
                {
                    spawnWave(slowEnemyProjectile, 1);
                    
                }
                break;
            case 7:
                
                    if (Random.Range(0f, 60f) <= 2)
                    {
                        spawnWave(homingMissile, 2, 1, 1.8f, 0.6f);

                    }
                    if (Random.Range(0f, 60f) <= 2)
                    {
                        spawnWave(homingMissile, 4, 1, 1.8f, 0.6f);

                    }

                
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
            whatEnemiesShouldSpawn();
        }

    }
}
