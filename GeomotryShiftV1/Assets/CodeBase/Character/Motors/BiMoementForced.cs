using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiMoementForced : CMotor
{
    public Rigidbody theRB;
    public float speedMultiplier_ = 6;

    void FixedUpdate()
    {
        Vector3 movementVector = new Vector3(0, 0, 0);
        movementVector.y = Input.GetAxis("Vertical");

        //Add forced X movemnt 
       // movementVector.x;

        movementVector.x *= speedMultiplier_;

        theRB.velocity = movementVector;

       

    }


    protected override void ConfigurePhysics()
    {
        theRB = GetComponent<Rigidbody>();
        theRB.constraints =
            RigidbodyConstraints.FreezePositionZ |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY;
        theRB.useGravity = false;
    }
}
