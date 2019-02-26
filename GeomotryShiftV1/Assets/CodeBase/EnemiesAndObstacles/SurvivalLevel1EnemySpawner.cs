using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalLevel1EnemySpawner : MonoBehaviour
{
    float secondsPassed = 0;
    int secondsPassedInt = 0;
    float enemySpawnTimer = 0;
    float weakProjectileScale = 0.82f;
    float strongProjectileScale = 1.3f;
    Random random = new Random();

    public Material orange;
    public Material red;

    public GameObject fastEnemyProjectile;//what projectile does the turret fire
    public GameObject slowEnemyProjectile;//what projectile does the turret fire
    public GameObject homingMissile;//what projectile does the turret fire
    public GameObject planarExplosion;//what projectile does the turret fire

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


    }

    void setPhase()
    {
        if (timeToPhase.ContainsKey(secondsPassedInt))
        {
            phase = (int)timeToPhase[secondsPassedInt];
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
                    float variance = 9.5f;

                    Vector3 spawnPosition = new Vector3(Random.Range(-variance, variance), 0f, -9f);
                    Quaternion spawnRotation = new Quaternion();
                    Instantiate(slowEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
                    
                }
                break;
            case 2:
                if (Random.Range(0f, 60f) <= 3)
                {
                    //slow
                    float variance = 9.5f;

                    Vector3 spawnPosition = new Vector3(Random.Range(-variance, variance), 0f, -9f);
                    Quaternion spawnRotation = new Quaternion();
                    Instantiate(slowEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
                }
                if (Random.Range(0f, 60f) <= 2)
                {
                    //fast
                    float variance = 9.5f;

                    Vector3 spawnPosition = new Vector3(Random.Range(-variance, variance), 0f, -9f);
                    Quaternion spawnRotation = new Quaternion();
                    spawnRotation = Quaternion.Euler(0f, Random.Range(-30f, 30f), 0f);
                    Instantiate(fastEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
                }
                break;
            case 3:
                if (Random.Range(0f, 60f) <= 3)
                {
                    //fast
                    float variance = 9.5f;

                    Vector3 spawnPosition = new Vector3(Random.Range(-variance, variance), 0f, -9f);
                    Quaternion spawnRotation = new Quaternion();
                    spawnRotation = Quaternion.Euler(0f, Random.Range(-40f, 40f), 0f);
                    Instantiate(fastEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
                }
                if (Random.Range(0f, 60f) <= 1.2)
                {
                    //planar explosion!
                    float variance = 9.5f;

                    Vector3 spawnPosition = new Vector3(Random.Range(-variance, variance), 0f, Random.Range(-variance, variance));
                    Quaternion spawnRotation = new Quaternion();
                    planarExplosion.GetComponent<PlanarExplosion>().theEnemyToSpawn = slowEnemyProjectile;
                    Instantiate(planarExplosion, spawnPosition, spawnRotation, transform.parent);
                }
                break;
            case 4:
                if (Random.Range(0f, 100f) <= 5)
                {
                    //homing missile
                    float variance = 9.5f;

                    Vector3 spawnPosition = new Vector3(Random.Range(-variance, variance), 0f, -9f);
                    Quaternion spawnRotation = new Quaternion();
                    spawnRotation = Quaternion.Euler(0f, Random.Range(-40f, 40f), 0f);
                    Instantiate(homingMissile, spawnPosition, spawnRotation, transform.parent);
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
