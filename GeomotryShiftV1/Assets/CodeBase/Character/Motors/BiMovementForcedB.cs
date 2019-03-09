using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiMovementForcedB : CMotor
{
    public Rigidbody theRB;
    public float jumpStrength_ = 45f;
    public float maxSpeed_ = 7.5f;
    public float speed_ = 0;
    bool availableJump = true;
    public float rayDistance = 1f;


    void FixedUpdate()
    {
        speed_ = rBody.velocity.x;
        if (speed_ >= maxSpeed_)
        {
            speed_ = maxSpeed_;
        }
        Vector3 movementVector = new Vector3(maxSpeed_-speed_, 0, 0);

        if (Input.GetKey(KeyCode.Space) && availableJump)
        {
            rBody.AddForce(rBody.transform.TransformDirection(Vector3.left) * jumpStrength_);
            availableJump = false;
        }

        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(rBody.transform.position, rBody.transform.TransformDirection(Vector3.right), rayDistance))
        {
            Debug.DrawRay(rBody.transform.position, rBody.transform.TransformDirection(Vector3.right) * rayDistance, Color.yellow);
           // Debug.Log("Did Hit");
            availableJump = true;
        }
        else
        {
            Debug.DrawRay(rBody.transform.position, rBody.transform.TransformDirection(Vector3.right) * rayDistance, Color.white);
           // Debug.Log("Did not Hit");
        }

        //movementVector.y = Input.GetAxis("Vertical") * jumpStrength_;




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
