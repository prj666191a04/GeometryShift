using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple3DMovement : CMotor
{

    Quaternion targetRotation;
    Rigidbody theRB;
    float speedMultiplier_ = 10;

    float sqrtOfZeroPointFive_ = 0.7071067811865475f;

    float dashTimeRemaining = 0f;
    float dashDuration = 0.26f;
    float dashSpeedMultiplier = 20;
    float dashCooldown = 0.40f;
    float dashCooldownRemaining = 0.40f;
    Vector3 dashDirection;

    public float rotSpeed = 10f;
    public float moveSpeed = 0f;
    public float startSpeed = 4f;
    public float maxSpeed = 10f;
    public float acellSpeed = 3f;
    public float decellSpeed = 3f;

    public bool hasWon = false;
    
    // Start is called before the first frame update
    void Start()
    {
        hasWon = false;
        theRB = GetComponent<Rigidbody>();
        theRB.constraints = RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezeRotationX |
        RigidbodyConstraints.FreezeRotationY;

        targetRotation = transform.rotation;

    }

    void RotateCharacter()
    {
        Vector3 targetDirection = new Vector3(
            Input.GetAxis("Horizontal"), 
            0, 
            Input.GetAxis("Vertical"));
        if (targetDirection != Vector3.zero)
        {
            //Set the target Direction
            targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        if (theRB == null)
        {
            theRB = GetComponent<Rigidbody>(); 
        }
        if (theRB)
        {
            if (transform.position.y < -2 && !hasWon)//player dies if they fall off the map
            {
                GetComponent<CStatus>().AbsoluteDamage(9999);
                print("you died from falling off the level (y position is less than -2)");
            }
            RotateCharacter();
            if (dashCooldownRemaining > 0)
            {
                dashCooldownRemaining -= Time.deltaTime;
            }
            if (dashTimeRemaining > 0)
            {
                Dash();
                return;
            }

            if (Input.GetKey("space") && dashCooldownRemaining <= 0)
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
                    //print("dash with x " + dashDirection.x + " and z " + dashDirection.z);

                    if (Mathf.Abs(dashDirection.x) > Mathf.Abs(dashDirection.z))
                    {
                        multiplier = dashSpeedMultiplier / Mathf.Abs(dashDirection.x);
                        //print("multi calculated from x is " + multiplier);
                        dashDirection.x *= multiplier;
                        dashDirection.z *= multiplier;
                    }
                    else
                    {
                        multiplier = dashSpeedMultiplier / Mathf.Abs(dashDirection.z);
                        //print("multi calculated from z is " + multiplier);
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
        }

    }
    void Dash()
    {
        theRB.velocity = dashDirection;
        dashTimeRemaining -= Time.deltaTime;
    }
}