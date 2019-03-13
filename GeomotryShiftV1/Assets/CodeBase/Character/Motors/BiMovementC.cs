using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiMovementC : CMotor
{
    public Rigidbody theRB;
    public float jumpStrength_ = 45f;
    public float maxSpeed_ = 7.5f;
    public float speed_ = 0;
    public float rayDistance = 1f;


    void FixedUpdate()
    {
        
        speed_ = rBody.velocity.x;
        if (speed_ >= maxSpeed_)
        {
            speed_ = maxSpeed_;
        }
        Vector3 movementVector = new Vector3(maxSpeed_ - speed_, 0, 0);

        if (Input.GetKey(KeyCode.Space))
        {
            rBody.AddForce(rBody.transform.TransformDirection(Vector3.left) * jumpStrength_);
        }

        rBody.AddForce(movementVector);
    }

    

    protected override void ConfigurePhysics()
    {
 
        rBody = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(new Vector3(180f, -90f, 90f));
        rBody.constraints =
            RigidbodyConstraints.FreezePositionZ |
            RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezePositionZ;


        rBody.useGravity = true;

        rBody.interpolation = RigidbodyInterpolation.Interpolate;

    }
}
