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
    Quaternion targetRotation;
    Vector3 targetDirection;
    CharacterController cc;

    public bool inversed = false;

    private void Start()
    {
        targetRotation = transform.rotation;
        targetDirection = Vector3.zero;
        
    }

    private void Update()
    {
        RotateCharacter();
    }



    void FixedUpdate()
    {
        
        speed_ = rBody.velocity.x;
        if (speed_ >= maxSpeed_)
        {
            speed_ = maxSpeed_;
        }
        Vector3 movementVector = new Vector3(maxSpeed_ - speed_, 0, 0);
        if(inversed)
        {
            movementVector = new Vector3( speed_ * -1  - maxSpeed_, 0, 0) ;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            //rBody.AddForce(rBody.transform.TransformDirection(Vector3.left) * jumpStrength_);
            rBody.AddForce(Vector3.up * jumpStrength_);
            controller_.DashEffect();
        }

        rBody.AddForce(movementVector);
    }

    //Inserted by Atilla
    void RotateCharacter()
    {
        targetDirection = rBody.velocity.normalized;

       // Vector3 heading = target - transform.position;

        if (targetDirection != Vector3.zero)
        {
            //Set the target Direction
            targetRotation = Quaternion.LookRotation(targetDirection, transform.up);

        }
        //Preform this frames rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10);
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
