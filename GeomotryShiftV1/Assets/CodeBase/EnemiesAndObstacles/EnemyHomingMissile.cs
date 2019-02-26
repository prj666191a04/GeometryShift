using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingMissile: MonoBehaviour
{
    public GameObject target = null;
    public Rigidbody missileRigidBody;

    public float damage = 2f;
    public float speed = 6;
    public float turnSpeed = 1;

    public float fuelTime = 2f;
    public float maximumLifespanAllowed = 4f;
    public float timeExistedInSeconds = 0f;
    public bool goThroughWalls = true;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CStatus>().Damage(damage);
            Destroy(gameObject);
        }

        if (!goThroughWalls)
        {
            if (!other.gameObject.name.Contains("Enemy"))//so it doesn't collide with other enemies
            {
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        missileRigidBody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        timeExistedInSeconds += Time.deltaTime;
        fuelTime -= Time.deltaTime;
        if (timeExistedInSeconds > maximumLifespanAllowed)
        {
            Destroy(gameObject);
        }
        //use of 2 opposite if statements instead of if else is intended

        if (target == null)
        {
            target = GeometryShift.playerStatus.gameObject;
        }
        if (target != null && fuelTime > 0)//rotate towards the target
        {
            missileRigidBody.velocity = transform.forward * speed * Time.deltaTime * 60;
            var rocketTargetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            missileRigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocketTargetRotation, turnSpeed));
        }
        if (fuelTime <= 0)
        {
            GetComponent<TrailRenderer>().emitting = false;
        }

    }
}
