using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoomerang : EnemyProjectile
{
    public float maxSpeed = 6f;
    Vector3 initialRotation;
    public float timeBeforeTurningAround = 1f;
    public float accelerationPerSecond = 5f;
    public float spinAnglesPerSecond = 360f;


    // Start is called before the first frame update
    void Start()
    {
        initialRotation = new Vector3(1f, 0f, 0f);
    }

    void BoomerangMove()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        //transform.Rotate(0f, spinAnglesPerSecond * Time.deltaTime, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        shouldDespawn();
        BoomerangMove();
        speed -= accelerationPerSecond * Time.deltaTime;
    }
}
