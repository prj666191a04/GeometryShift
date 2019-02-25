using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalLevel1EnemySpawner : MonoBehaviour
{
    float secondsPassed = 0;
    float enemySpawnTimer = 0;
    float weakProjectileScale = 0.82f;
    float strongProjectileScale = 1.3f;
    Random random = new Random();

    public Material orange;
    public Material red;

    public GameObject basicEnemyProjectile;//what projectile does the turret fire
    public GameObject slowEnemyProjectile;//what projectile does the turret fire

    Hashtable timeToPhase;

    // Start is called before the first frame update
    void Start()
    {
        
        basicEnemyProjectile = Resources.Load("Enemies/EnemyTurretFolder/Enemy Projectile") as GameObject;
        basicEnemyProjectile.gameObject.GetComponent<EnemyProjectile>().maximumLifespanAllowed = 12;


        slowEnemyProjectile = Resources.Load("Enemies/EnemyTurretFolder/Enemy Projectile") as GameObject;
        slowEnemyProjectile.gameObject.GetComponent<EnemyProjectile>().maximumLifespanAllowed = 12;

        timeToPhase = new Hashtable();

        timeToPhase.Add(1, "One");
        timeToPhase.Add(2, "Teu");
        timeToPhase.Add(3, "Threa!");


    }

    void whatEnemiesShouldSpawn()//60 times a second, no matter the FPS
    {
        print("key is 1, value is " + timeToPhase[1]);
        print("key is 2, value is " + timeToPhase[2]);
        print("key is 3, value is " + timeToPhase[3]);
        print("key is 4, value is " + timeToPhase[4]);
        if (Random.Range(0f, 11f) <= 0.8)
        {
            //slow
            slowEnemyProjectile.gameObject.GetComponent<EnemyProjectile>().speed = 5f + secondsPassed / 3f;
            
            float variance = 9.5f;

            Vector3 spawnPosition = new Vector3(Random.Range(-variance, variance), 0f, -9f);
            Quaternion spawnRotation = new Quaternion();
            Instantiate(slowEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
        }
        else if (Random.Range(0f, 11f) <= 0.2)
        {
            //fast
            basicEnemyProjectile.gameObject.GetComponent<EnemyProjectile>().speed = 9f + secondsPassed / 2f;
            
            float variance = 9.5f;

            Vector3 spawnPosition = new Vector3(Random.Range(-variance, variance), 0f, -9f);
            Quaternion spawnRotation = new Quaternion();
            spawnRotation = Quaternion.Euler(0f, Random.Range(-30f, 30f), 0f);
            Instantiate(basicEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        secondsPassed += Time.deltaTime;
        enemySpawnTimer += Time.deltaTime;

        while (enemySpawnTimer > 0.0167f) // to make enemy spawn function run 60 times per second
                                          //even when FPS is above or below 60
        {
            enemySpawnTimer -= 0.0167f;
            whatEnemiesShouldSpawn();
        }

    }
}
