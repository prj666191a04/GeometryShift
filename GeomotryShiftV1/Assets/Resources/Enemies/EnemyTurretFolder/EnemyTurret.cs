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


    public GameObject currentHitObject;
    private Vector3 origin;
    public float sphereRadius;
    private Vector3 direction;
    public float maxDistance;
    public LayerMask layerMask;

    void Shoot()
    {
        thePrefab.GetComponent<EnemyProjectile>().speed = projectileSpeed;
        if (trackTarget)
        {
            origin = transform.position;
            direction = transform.forward;
            RaycastHit[] raycastHits;
            raycastHits = Physics.SphereCastAll(origin, sphereRadius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);

            theTarget = null;

            foreach (RaycastHit theHit in raycastHits)
            {
                print("spherecast hit " + theHit.transform.gameObject.name);
                if (theHit.transform.gameObject.name.Contains("Player"))
                {
                    theTarget = theHit.transform.gameObject;
                }
            }
            if (theTarget == null)
            {
                //no targets in sight
            }
            else
            {
                transform.LookAt(theTarget.transform);                                               //look at
                Instantiate(thePrefab, transform.position, transform.rotation, transform.parent);    //shoot
            }
        }
        else
        {
            Instantiate(thePrefab, transform.position, transform.rotation, transform.parent);    //shoot

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        thePrefab = Resources.Load("Enemies/EnemyTurretFolder/Enemy Projectile") as GameObject;
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
