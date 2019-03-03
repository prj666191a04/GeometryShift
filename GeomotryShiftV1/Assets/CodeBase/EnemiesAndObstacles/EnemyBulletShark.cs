using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletShark : EnemyHomingMissile
{
    public float overrideSpeed = 8f;
    public float overrideTurnSpeed = 0.5f;
    public float overrideFuelTime = 999f;
    public int numberOfProjectilesToShoot = 2;

    public float shootInterval = 0.7f;
    float timeSinceLastShot = 0f;

    float baseAngle;
    float actualAngle;
    GameObject planarExplosion;
    public GameObject whatToShoot;

    // Start is called before the first frame update
    void Start()
    {
        goThroughWalls = true;

        baseAngle = 180f / numberOfProjectilesToShoot;

        turnSpeed = overrideTurnSpeed;
        speed = overrideSpeed;
        fuelTime = overrideFuelTime;

        planarExplosion = Resources.Load("Enemies/PlanarExplosion/PlanarExplosion") as GameObject;

        if (whatToShoot == null)
        {
            //whatToShoot = Resources.Load("Enemies/EnemyTurretFolder/Enemy Projectile Slow") as GameObject;
            whatToShoot = Resources.Load("Enemies/EnemyTurretFolder/Enemy Projectile") as GameObject;
        }

        planarExplosion.GetComponent<PlanarExplosion>().theEnemyToSpawn = whatToShoot;
        planarExplosion.GetComponent<PlanarExplosion>().numberOfEnemiesSpawned = numberOfProjectilesToShoot;
        missileRigidBody = GetComponent<Rigidbody>();
    }

    void SpawnBullets()
    {
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot > shootInterval)
        {
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
