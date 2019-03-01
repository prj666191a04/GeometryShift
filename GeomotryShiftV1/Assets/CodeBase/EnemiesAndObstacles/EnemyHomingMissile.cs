using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingMissile: EnemyProjectile
{

    public GameObject target = null;
    public Rigidbody missileRigidBody;

    //public float damage = 2f;
    //public float speed = 6;
    public float turnSpeed = 1f;

    public float fuelTime = 2f;
    //public float maximumLifespanAllowed = 4f;
    //public float timeExistedInSeconds = 0f;
    //public bool goThroughWalls = true;
    

    // Start is called before the first frame update
    void Start()
    {
        missileRigidBody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        shouldDespawn();//inherited from EnemyProjectile

        //use of 2 opposite if statements instead of if else is intended

        if (target == null)
        {
            target = GeometryShift.playerStatus.gameObject;
        }
        if (target != null && fuelTime > 0)//rotate towards the target
        {
            fuelTime -= Time.deltaTime;

            //where should it rotate towards?
            var rocketTargetRotation = Quaternion.LookRotation(target.transform.position - transform.position);

            //rotate towards it
            missileRigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocketTargetRotation, turnSpeed));
        }
        
        MoveForward();//inherited from EnemyProjectile

        if (fuelTime <= 0)
        {
            GetComponent<TrailRenderer>().emitting = false;
        }

    }
}
