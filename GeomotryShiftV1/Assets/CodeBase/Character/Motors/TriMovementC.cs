using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Created by Harley - Implementing the 3D movement from Allan
public class TriMovementC : CMotor   
{
    public Rigidbody theRB;
    public float speedMultiplier_ = 6;
    public float jumpForce_ = 5.5f;
    public int numberOfMidairJumpsAllowed_ = 0;//0 = no midair jump, 1 = double jump, 2 = triple jump, etc
    int numberOfMidairJumpsRemaining_ = 0;
    float sqrtOfZeroPointFive_ = 0.7071067811865475f;

    float dashTimeRemaining = 0f;
    public float dashDuration = 0.26f;
    public float dashSpeedMultiplier = 18;
    public float dashCooldown = 0.40f;
    float dashCooldownRemaining = 0.40f;
    Vector3 dashDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void FixedUpdate()
    {
        if (dashCooldownRemaining > 0)
        {
            dashCooldownRemaining -= Time.deltaTime;
        }
        if (dashTimeRemaining > 0)
        {
            Dash();
            return;
        }

        if (Input.GetKey("left shift") && dashCooldownRemaining <= 0)
        {
            dashCooldownRemaining = dashCooldown;

            dashDirection = new Vector3(0, 0, 0);
            dashDirection.x = Input.GetAxis("Horizontal");
            dashDirection.y = 0;
            dashDirection.z = Input.GetAxis("Vertical");

            if (dashDirection.x == 0 && dashDirection.z == 0)
            {
                //nothing
            }
            else
            {
                float multiplier = dashSpeedMultiplier;
                print("dash with x " + dashDirection.x + " and z " + dashDirection.z);

                if (Mathf.Abs(dashDirection.x) > Mathf.Abs(dashDirection.z))
                {
                    multiplier = dashSpeedMultiplier / Mathf.Abs(dashDirection.x);
                    print("multi calculated from x is " + multiplier);
                    dashDirection.x *= multiplier;
                    dashDirection.z *= multiplier;
                }
                else
                {
                    multiplier = dashSpeedMultiplier / Mathf.Abs(dashDirection.z);
                    print("multi calculated from z is " + multiplier);
                    dashDirection.x *= multiplier;
                    dashDirection.z *= multiplier;
                }


                dashTimeRemaining = dashDuration;
                Dash();
                return;
            }
        }

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

    void Dash()
    {
        theRB.velocity = dashDirection;
        dashTimeRemaining -= Time.deltaTime;

        //this one caused some clipping through objects
        //transform.position += dashDirection * Time.deltaTime;
    }

    protected override void ConfigurePhysics()
    { 
        theRB = GetComponent<Rigidbody>();
    }

}
