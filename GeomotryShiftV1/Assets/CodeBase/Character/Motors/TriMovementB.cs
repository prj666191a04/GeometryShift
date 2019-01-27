using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script used to test motor change
public class TriMovementB : CMotor
{
    public float speedMultiplier = 6;
    public float jumpForce = 8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rBody.MovePosition(transform.position + (transform.forward * v_ + transform.right * h_) * (Time.deltaTime * 20));
        ////Quaternion TargetRotation = transform.rotation.;
        //Vector3 myVector = new Vector3(0, 0, 0);
        //myVector.x = h_;
        //myVector.y = rBody.velocity.y;
        //myVector.z = v_;
        //myVector.x *= speedMultiplier;
        //myVector.z *= speedMultiplier;
        //this.rBody.velocity = myVector;
    }

    protected override void ConfigurePhysics()
    {
        rBody.useGravity = true;
        rBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        rBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rBody.interpolation = RigidbodyInterpolation.Interpolate;

    }
}
