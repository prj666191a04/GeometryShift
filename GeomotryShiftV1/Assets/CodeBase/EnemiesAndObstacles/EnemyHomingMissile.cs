using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingMissile: MonoBehaviour
{
    public GameObject target = null;
    public Rigidbody missileRigidBody;

    public float turnSpeed = 1;
    public float speed = 6;

    public GameObject currentHitObject;
    private Vector3 origin;
    public float sphereRadius = 100;
    private Vector3 direction;
    public float maxDistance = 0;
    public LayerMask layerMask;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            origin = transform.position;
            direction = transform.forward;
            RaycastHit[] raycastHits;
            raycastHits = Physics.SphereCastAll(origin, sphereRadius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);

            foreach (RaycastHit theHit in raycastHits)
            {
                if (theHit.transform.gameObject.name.Contains("Player"))
                {
                    target = theHit.transform.gameObject;
                    break;
                }
            }
        }
   

        missileRigidBody.velocity = transform.forward * speed;

        var rocketTargetRotation = Quaternion.LookRotation(target.transform.position - transform.position);

        missileRigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocketTargetRotation, turnSpeed));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
