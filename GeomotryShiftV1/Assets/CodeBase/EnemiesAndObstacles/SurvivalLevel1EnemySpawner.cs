using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalLevel1EnemySpawner : MonoBehaviour
{
    float secondsPassed = 0;
    float enemySpawnTimer = 0;
    Random random = new Random();

    public GameObject basicEnemyProjectile;//what projectile does the turret fire


    // Start is called before the first frame update
    void Start()
    {
        basicEnemyProjectile = Resources.Load("Enemies/EnemyTurretFolder/Enemy Projectile") as GameObject;
        basicEnemyProjectile.gameObject.GetComponent<EnemyProjectile>().maximumLifespanAllowed = 12;
        
    }

    void whatEnemiesShouldSpawn()//60 times a second, no matter the FPS
    {
        if (Random.Range(0f, 11f) <= 1)
        {
            basicEnemyProjectile.gameObject.GetComponent<EnemyProjectile>().speed = 5f + secondsPassed/3f;
            float variance = 9.5f;
            
            Vector3 spawnPosition = new Vector3(Random.Range(-variance, variance), 0f, -9f);
            Quaternion spawnRotation = new Quaternion();
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
