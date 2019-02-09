using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Created by Harley - Implementing the 2D movement from Allan
public class BiMovementA : CMotor
{
    public Rigidbody theRB;
    public float speedMultiplier_ = 6;
    public float jumpForce_ = 10.5f;
    public int numberOfMidairJumpsAllowed_ = 1;//0 = no midair jump, 1 = double jump, 2 = triple jump, etc
    public int numberOfMidairJumpsRemaining_ = 1;
    float sqrtOfZeroPointFive_ = 0.7071067811865475f;

    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
         Vector3 movementVector = new Vector3(0, 0, 0);
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = theRB.velocity.y;
        movementVector.z = Input.GetAxis("Vertical");

        movementVector.x *= speedMultiplier_;
        movementVector.z *= speedMultiplier_;

        theRB.velocity = movementVector;

        bool isGroundedThisFrame = isGrounded();//so it only runs once per frame

        if (isGroundedThisFrame)//reset the number of air jumps
        {
            numberOfMidairJumpsRemaining_ = numberOfMidairJumpsAllowed_;
        }

        if (Input.GetButtonDown("Jump"))//the player attempts to jump
        {
            if (isGroundedThisFrame)//the player can jump because they are on the ground
            {
                theRB.velocity = new Vector3(theRB.velocity.x, jumpForce_, theRB.velocity.z);
            }
            else if (numberOfMidairJumpsRemaining_ > 0)//the player can air-jump
            {
                theRB.velocity = new Vector3(theRB.velocity.x, jumpForce_, theRB.velocity.z);
                numberOfMidairJumpsRemaining_--;
            }
        }

    }

    bool isGrounded()
    {
        float compensationRange = 0.7f;
        float distanceToGroundToQualityAsGrounded = 1.1f;
        Vector3 positionVector = new Vector3(0, 0, 0);
        Vector3 originalPositionVector = new Vector3(0, 0, 0);
        positionVector = transform.position;
        originalPositionVector = transform.position;
        if (Physics.Raycast(positionVector, -Vector3.up, distanceToGroundToQualityAsGrounded))
        {
            return true;
        }


        //compensation ground detection below
        //explained in compensationRangeExplanation.png

        positionVector.x += compensationRange;
        if (Physics.Raycast(positionVector, -Vector3.up, distanceToGroundToQualityAsGrounded))
        {
            return true;
        }

        positionVector = originalPositionVector;
        positionVector.x -= compensationRange;
        if (Physics.Raycast(positionVector, -Vector3.up, distanceToGroundToQualityAsGrounded))
        {
            return true;
        }

        positionVector = originalPositionVector;
        positionVector.z += compensationRange;
        if (Physics.Raycast(positionVector, -Vector3.up, distanceToGroundToQualityAsGrounded))
        {
            return true;
        }

        positionVector = originalPositionVector;
        positionVector.z -= compensationRange;
        if (Physics.Raycast(positionVector, -Vector3.up, distanceToGroundToQualityAsGrounded))
        {
            return true;
        }

        //now the diagonals

        positionVector = originalPositionVector;
        positionVector.x += compensationRange * sqrtOfZeroPointFive_;
        positionVector.z += compensationRange * sqrtOfZeroPointFive_;
        if (Physics.Raycast(positionVector, -Vector3.up, distanceToGroundToQualityAsGrounded))
        {
            return true;
        }

        positionVector = originalPositionVector;
        positionVector.x += compensationRange * 0.7f;
        positionVector.z -= compensationRange * sqrtOfZeroPointFive_;
        if (Physics.Raycast(positionVector, -Vector3.up, distanceToGroundToQualityAsGrounded))
        {
            return true;
        }

        positionVector = originalPositionVector;
        positionVector.x -= compensationRange * 0.7f;
        positionVector.z += compensationRange * sqrtOfZeroPointFive_;
        if (Physics.Raycast(positionVector, -Vector3.up, distanceToGroundToQualityAsGrounded))
        {
            return true;
        }

        positionVector = originalPositionVector;
        positionVector.x -= compensationRange * 0.7f;
        positionVector.z -= compensationRange * sqrtOfZeroPointFive_;
        if (Physics.Raycast(positionVector, -Vector3.up, distanceToGroundToQualityAsGrounded))
        {
            return true;
        }


        return false;
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
