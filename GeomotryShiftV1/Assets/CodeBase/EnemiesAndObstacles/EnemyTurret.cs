using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    float secondsPassed = 0f;
    public float attackInterval = 0.2f;
    public float projectileSpeed = 12f;
    public GameObject thePrefab;//what projectile does the turret fire
    public GameObject theTarget;//optional
    public bool trackTarget = false;
    public float degreesToTurnPerSecond = 180f;

    public float maxRange = 8;
    

    void Shoot()
    {
        thePrefab.GetComponent<EnemyProjectile>().speed = projectileSpeed;
        if (trackTarget)
        {


            //using two opposite if statements instead of if else is intended
            if (theTarget == null)
            {
                theTarget = GeometryShift.playerStatus.gameObject;
                
            }

            if (theTarget != null)
            {
                float dist = Vector3.Distance(theTarget.transform.position, transform.position);
                if (dist <= maxRange)
                {
                    transform.LookAt(theTarget.transform);                                               //look at
                    Instantiate(thePrefab, transform.position, transform.rotation, transform.parent);    //shoot
                }
            }
        }
        else
        {
            Instantiate(thePrefab, transform.position, transform.rotation, transform.parent);    //shoot

        }

    }

    // Start is called before the first frame update
    /*
    void Start()
    {
        if (thePrefab == null)
        {
            thePrefab = Resources.Load("Enemies/EnemyTurretFolder/Enemy Projectile") as GameObject;
        }
    }
    */
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
