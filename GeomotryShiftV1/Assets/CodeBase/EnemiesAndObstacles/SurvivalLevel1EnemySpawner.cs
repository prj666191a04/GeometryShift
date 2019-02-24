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


    // Start is called before the first frame update
    void Start()
    {
        basicEnemyProjectile = Resources.Load("Enemies/EnemyTurretFolder/Enemy Projectile") as GameObject;
        basicEnemyProjectile.gameObject.GetComponent<EnemyProjectile>().maximumLifespanAllowed = 12;

    }

    void whatEnemiesShouldSpawn()//60 times a second, no matter the FPS
    {
        
        if (Random.Range(0f, 11f) <= 0.8)
        {
            basicEnemyProjectile.gameObject.GetComponent<EnemyProjectile>().speed = 5f + secondsPassed / 3f;
            basicEnemyProjectile.gameObject.GetComponent<MeshRenderer>().material = orange;

            basicEnemyProjectile.gameObject.transform.localScale = new Vector3(weakProjectileScale, weakProjectileScale, weakProjectileScale);

            float variance = 9.5f;

            Vector3 spawnPosition = new Vector3(Random.Range(-variance, variance), 0f, -9f);
            Quaternion spawnRotation = new Quaternion();
            Instantiate(basicEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
        }
        else if (Random.Range(0f, 11f) <= 0.2)
        {
            basicEnemyProjectile.gameObject.GetComponent<EnemyProjectile>().speed = 9f + secondsPassed / 2f;
            basicEnemyProjectile.gameObject.GetComponent<MeshRenderer>().material = red;

            basicEnemyProjectile.gameObject.transform.localScale = new Vector3(strongProjectileScale, strongProjectileScale, strongProjectileScale);



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
