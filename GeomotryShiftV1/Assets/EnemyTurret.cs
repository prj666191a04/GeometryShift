using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    float secondsPassed = 0f;
    public float attackInterval = 0.2f;
    public float projectileSpeed = 12f;
    public GameObject thePrefab;
    public GameObject theTarget;
    public bool trackTarget = false;
    public float degreesToTurnPerSecond = 180f;

    void Shoot()
    {
        thePrefab.GetComponent<EnemyProjectile>().speed = projectileSpeed;
        if (trackTarget)
        {
            transform.LookAt(theTarget.transform);
        }
        Instantiate(thePrefab, transform.position, transform.rotation, transform.parent);
    }

    // Start is called before the first frame update
    void Start()
    {
        thePrefab = Resources.Load("Enemies/Enemy Projectile") as GameObject;
    }

    // Update is called once per frame
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
