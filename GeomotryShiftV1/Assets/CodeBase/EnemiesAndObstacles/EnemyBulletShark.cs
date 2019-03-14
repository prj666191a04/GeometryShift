using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletShark : EnemyHomingMissile
{
    public int numberOfProjectilesToShoot = 2;

    public float shootInterval = 0.7f;
    float timeSinceLastShot = 0f;

    float baseAngle;
    float actualAngle;
    public GameObject planarExplosion;
    PlanarExplosion peScript;
    public GameObject whatToShoot;

    // Start is called before the first frame update
    void Start()
    {
        if (planarExplosion == null)
        {
            planarExplosion = Resources.Load("Enemies/PlanarExplosion/PlanarExplosion") as GameObject;
        }
        if (whatToShoot == null)
        {
            whatToShoot = Resources.Load("Enemies/EnemyTurretFolder/Enemy Projectile") as GameObject;
        }
        peScript = planarExplosion.GetComponent<PlanarExplosion>();
        goThroughWalls = true;

        baseAngle = 180f / numberOfProjectilesToShoot;



        peScript.theEnemyToSpawn = whatToShoot;
        peScript.numberOfEnemiesSpawned = numberOfProjectilesToShoot;
        missileRigidBody = GetComponent<Rigidbody>();
    }

    void SpawnBullets()
    {
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot > shootInterval)
        {
            peScript.theEnemyToSpawn = whatToShoot;
            peScript.numberOfEnemiesSpawned = numberOfProjectilesToShoot;
            timeSinceLastShot -= shootInterval;
            actualAngle = baseAngle + transform.rotation.eulerAngles.y;
            planarExplosion.GetComponent<PlanarExplosion>().maximumLifespanAllowed = 0f;
            Instantiate(planarExplosion, transform.position, Quaternion.Euler(0f, actualAngle, 0f), transform.parent);
        }
    }



    // Update is called once per frame
    void Update()
    {
        shouldDespawn();//inherited from EnemyProjectile
        RotateTowardsTarget();//inherited from EnemyHomingMissile
        SpawnBullets();
        MoveForward();//inherited from EnemyProjectile
    }
}
