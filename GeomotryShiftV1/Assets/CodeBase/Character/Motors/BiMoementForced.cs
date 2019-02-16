using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiMoementForced : CMotor
{
    public Rigidbody theRB;
    public float speedMultiplier_ = 6;

    void FixedUpdate()
    {
        Vector3 movementVector = new Vector3(speedMultiplier_, 0, 0);

        movementVector.y = Input.GetAxis("Vertical")*speedMultiplier_;

        theRB.velocity = movementVector;

    }


    protected override void ConfigurePhysics()
    {
        theRB = GetComponent<Rigidbody>();
        theRB.constraints =
            RigidbodyConstraints.FreezePositionZ |
            RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezePositionZ;
        
        theRB.useGravity = false;
    }
}
