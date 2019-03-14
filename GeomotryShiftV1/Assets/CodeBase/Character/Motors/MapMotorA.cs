using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMotorA : CMotor
{
    public Vector3 movementVector;

    public Vector3 velocityDirection;
    public Vector3 normalizedMovementDirection;

    LayerMask mask;

  
    Quaternion targetRotation;

    public float rotSpeed = 10f;
    public float moveSpeed = 0f;
    public float startSpeed = 4f;
    public float maxSpeed = 10f;
    public float acellSpeed = 3f;
    public float decellSpeed = 3f;
    private void Start()
    {
        mask = LevelLoader.instance.wMapColLayer;
        targetRotation = transform.rotation;
    }

    private void Update()
    {
        RotateCharacter();
    }
    private void FixedUpdate()
    {
        MoveCharacter();
    }
    void MoveCharacter()
    {
        Vector3 input = new Vector3(h_, 0, v_);
        if(input != Vector3.zero)
        {
            movementVector = input;
            Accelerate();
        }
        else
        {
            Deccelerate();
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, movementVector, out hit, (moveSpeed * Time.deltaTime) + 0.6f , mask))
        {
            rBody.MovePosition(transform.position + movementVector * (hit.distance - 0.6f));
            Debug.Log("wall");
        }
        else
        {
            rBody.MovePosition(transform.position + (movementVector * moveSpeed) * Time.deltaTime);
        }
    }

    void Accelerate()
    {
        if(moveSpeed < startSpeed)
        {
            moveSpeed = startSpeed;
        }
        else
        {
            moveSpeed += Time.deltaTime * acellSpeed;
            if(moveSpeed > maxSpeed)
            {
                moveSpeed = maxSpeed;
            }
        }
    }
    void Deccelerate()
    {
        moveSpeed -= Time.deltaTime * (decellSpeed + moveSpeed);
        if(moveSpeed < 0)
        {
            moveSpeed = 0;
        }
    }

    void RotateCharacter()
    {
        Vector3 targetDirection = new Vector3(h_, 0, v_);
        if (targetDirection != Vector3.zero)
        {
            //Set the target Direction
            targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed);

    }
    protected override void ConfigurePhysics()
    {
        rBody.useGravity = true;
        rBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        rBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rBody.interpolation = RigidbodyInterpolation.None;
        rBody.mass = 1;
    }

 
}
