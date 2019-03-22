using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Modified code from allans original turret code
// Deleted a lot of useless code and set it so you no longer use resource.load
public class TurretBehaviour : MonoBehaviour
{
    float secondsPassed = 0f;
    public float attackInterval = 0.2f;
    public float projectileSpeed = 12f;
    public GameObject thePrefab;//what projectile does the turret fire
    public float degreesToTurnPerSecond = 180f;

    void Shoot()
    {
        thePrefab.GetComponent<EnemyProjectile>().speed = projectileSpeed;
    }

    void Update()
    {
        transform.Rotate(0f, degreesToTurnPerSecond * Time.deltaTime, 0f);
        secondsPassed += Time.deltaTime;
        if (secondsPassed > attackInterval)
        {
            secondsPassed = 0;
            Shoot();
        }
    }
}
