using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanarExplosion : MonoBehaviour
{
    public float maximumLifespanAllowed = 1f;
    public float timeExistedInSeconds = 0f;
    public float angle = 0f;
    public int numberOfEnemiesSpawned = 6;

    public GameObject theEnemyToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        if (theEnemyToSpawn == null)
        {
            theEnemyToSpawn = Resources.Load("Enemies/EnemyTurretFolder/Enemy Projectile") as GameObject;
        }   
    }

    void Explode()
    {
        float angleInterval = 360f / numberOfEnemiesSpawned;
        float currentAngle = 0;

        Quaternion spawnRotation = new Quaternion();
        for (int i = 0; i < numberOfEnemiesSpawned; i++)
        {
            spawnRotation = Quaternion.Euler(0f, currentAngle, 0f);
            currentAngle += angleInterval;
            Instantiate(theEnemyToSpawn, this.transform.position, spawnRotation, transform.parent);
        }

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        timeExistedInSeconds += Time.deltaTime;
        if (timeExistedInSeconds > maximumLifespanAllowed)
        {
            Explode();
        }
    }
}
