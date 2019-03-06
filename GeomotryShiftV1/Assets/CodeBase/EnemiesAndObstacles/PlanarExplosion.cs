using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanarExplosion : MonoBehaviour
{
    public float maximumLifespanAllowed = 1f;
    public float timeExistedInSeconds = 0f;
    //public float angle = 0f;
    public int numberOfEnemiesSpawned = 6;
    
    public GameObject theEnemyToSpawn;
    public GameObject slowEnemyProjectile;

    // Start is called before the first frame update
    void Start()
    {
        if (theEnemyToSpawn == null)
        {
            theEnemyToSpawn = slowEnemyProjectile;
        }   

        if (maximumLifespanAllowed <= 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        float angleInterval = (float)(360f / numberOfEnemiesSpawned);
        float currentAngle = transform.rotation.eulerAngles.y;

        Quaternion spawnRotation;

        for (int i = 0; i < numberOfEnemiesSpawned; i++)
        {
            spawnRotation = Quaternion.Euler(0f, currentAngle, 0f);
            currentAngle += angleInterval;
            Instantiate(theEnemyToSpawn, transform.position, spawnRotation, transform.parent);
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
