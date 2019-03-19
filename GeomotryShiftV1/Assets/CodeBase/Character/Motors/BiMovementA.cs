using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Created by Harley - Implementing the 2D movement from Allan
public class BiMovementA : CMotor
{
    public Rigidbody theRB;
    public float speedMultiplier_ = 6;
    public float jumpForce_ = 10.5f;
    public int numberOfJumpsAllowed_ = 2;
    public int numberOfJumpsRemaining_ = 2;

    public float rayDistance = 0.425f;
    public float rayDistanceDiag = 0.51f;

    bool availableJump_ = true;

    void FixedUpdate()
    {

        Vector3 movementVector = new Vector3(0, 0, 0);
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = theRB.velocity.y;
        movementVector.z = Input.GetAxis("Vertical");

        movementVector.x *= speedMultiplier_;
        movementVector.z *= speedMultiplier_;

        theRB.velocity = movementVector;

        isGrounded(); 

        if (Input.GetButtonDown("Jump"))//the player attempts to jump
        {
            if (availableJump_)//the player can jump because they are on the ground
            {
                theRB.velocity = new Vector3(theRB.velocity.x, jumpForce_, theRB.velocity.z);
                numberOfJumpsRemaining_--;
                if (numberOfJumpsRemaining_ == 0)
                {
                    availableJump_ = false;
                }
            }
        }
    }

    // Checks if player is on the ground and 
    void isGrounded()
    {
         // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(rBody.transform.position, rBody.transform.TransformDirection(Vector3.down), rayDistance)
            || Physics.Raycast(rBody.transform.position, rBody.transform.TransformDirection(Quaternion.Euler(0, 0, -34) * Vector3.down), rayDistanceDiag)
            || Physics.Raycast(rBody.transform.position, rBody.transform.TransformDirection(Quaternion.Euler(0, 0, 34) * Vector3.down), rayDistanceDiag))
        {
           // Debug.DrawRay(rBody.transform.position, rBody.transform.TransformDirection(Vector3.down) * rayDistance, Color.yellow);
            //Debug.DrawRay(rBody.transform.position, rBody.transform.TransformDirection(Quaternion.Euler(0, 0, -34) * Vector3.down) * rayDistanceDiag, Color.yellow);
            //Debug.DrawRay(rBody.transform.position, rBody.transform.TransformDirection(Quaternion.Euler(0, 0, 34) * Vector3.down) * rayDistanceDiag, Color.yellow);
            // Debug.Log("Did Hit");
            availableJump_ = true;
            numberOfJumpsRemaining_ = numberOfJumpsAllowed_;
        }
        else
        {
            //Debug.DrawRay(rBody.transform.position, rBody.transform.TransformDirection(Vector3.down) * rayDistance, Color.white);
            // Debug.Log("Did not Hit");
        }


    }


    protected override void ConfigurePhysics()
    {
        theRB = GetComponent<Rigidbody>();
        theRB.constraints =
            RigidbodyConstraints.FreezePositionZ |
            RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;

    }
   
}
