using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiMovementForcedB : CMotor
{
    public Rigidbody theRB;
    public float speedMultiplier_ = 6f;
    public float jumpStrength_ = 30f;

    void FixedUpdate()
    {
        Vector3 movementVector = new Vector3(speedMultiplier_, 0, 0);

        movementVector.y = Input.GetAxis("Vertical") * jumpStrength_;

        rBody.AddForce(movementVector);

    }


    protected override void ConfigurePhysics()
    {
        //rBody = GetComponent<Rigidbody>();
        rBody.constraints =
            RigidbodyConstraints.FreezePositionZ |
            RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezePositionZ;

        //rBody.rotation.Equals(Quaternion.Euler(new Vector3(0, 0, 0)));

        rBody.useGravity = false;
        rBody.interpolation = RigidbodyInterpolation.Interpolate;
        
    }
}
