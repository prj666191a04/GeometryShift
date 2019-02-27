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

        timeToPhase.Add(5, 2);
        timeToPhase.Add(10, 3);
        timeToPhase.Add(15, 4);
        timeToPhase.Add(20, 5);
        timeToPhase.Add(25, 6);
        timeToPhase.Add(30, 7);


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

    void spawnPlanarExplosion(GameObject projectile)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, Random.Range(-(lengthOfLevel / 2), (lengthOfLevel / 2)));
        Quaternion spawnRotation = new Quaternion();
        planarExplosion.GetComponent<PlanarExplosion>().theEnemyToSpawn = projectile;
        Instantiate(planarExplosion, spawnPosition, spawnRotation, transform.parent);
    }

    void spawnWave(GameObject projectile,
        int side = 3, //1 = top, 2 = right, 3 = bottom, 4 = left
        int numberOfFlankingProjectilesOnEachSide = 2,
        float widthSpacing = 0.6f,
        float distanceSpacing = 0.2f)
    {
        
        float x = Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2));
        float y = -(lengthOfLevel / 2);

        Vector3 spawnPosition = new Vector3(x, 0f, y);
        Quaternion spawnRotation = new Quaternion();

        Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);

        for (int i = 1; i <= numberOfFlankingProjectilesOnEachSide; i++)
        {
            spawnPosition = new Vector3(x + (i * widthSpacing), 0f, y - (i * distanceSpacing));
            Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);
            spawnPosition = new Vector3(x - (i * widthSpacing), 0f, y - (i * distanceSpacing));
            Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);
        }

    }

    void whatEnemiesShouldSpawn()//60 times a second, no matter the FPS
    {
        switch (phase)
        {
            case 1:
                if (Random.Range(0f, 60f) <= 5)
                {
                    //slow

                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    Instantiate(slowEnemyProjectile, spawnPosition, spawnRotation, transform.parent);

                }
                break;
            case 2:
                if (Random.Range(0f, 60f) <= 3)
                {
                    //slow

                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    Instantiate(slowEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
                }
                if (Random.Range(0f, 60f) <= 2)
                {
                    //fast

                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    spawnRotation = Quaternion.Euler(0f, Random.Range(-30f, 30f), 0f);
                    Instantiate(fastEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
                }
                break;
            case 3:
                if (Random.Range(0f, 60f) <= 3)
                {
                    //fast

                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    spawnRotation = Quaternion.Euler(0f, Random.Range(-40f, 40f), 0f);
                    Instantiate(fastEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
                }
                secondsSinceLastPlanarExplosion += Time.deltaTime;
                if (secondsSinceLastPlanarExplosion > secondsBetweenEachPlanarExplosion)
                {
                    //planar explosion
                    secondsSinceLastPlanarExplosion -= secondsBetweenEachPlanarExplosion;
                    spawnPlanarExplosion(slowEnemyProjectile);
                }
                break;
            case 4:
                if (Random.Range(0f, 60f) <= 3)
                {
                    //homing missile

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
                    //planar explosion
                    secondsSinceLastPlanarExplosion -= secondsBetweenEachPlanarExplosion;
                    spawnPlanarExplosion(homingMissile);
                }
                break;
            case 6:
                //waves

                if (Random.Range(0f, 60f) <= 5)
                {
                    spawnWave(slowEnemyProjectile);
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
