using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatMotorA : CMotor
{

    Quaternion newRotation;
    Quaternion targetRotation;
    Vector3 targetDirection;

    private void Start()
    {
        newRotation = transform.rotation;
        targetRotation = transform.rotation;
    }

    private void Update()
    {
        RotateToDirection();
    }

    void FixedUpdate()
    {
        rBody.MovePosition(transform.position + new Vector3(h_, 0, v_) * 0.3f);
        
    }

    void RotateToDirection()
    {

        //Get direction of input
        targetDirection = new Vector3(h_, 0, v_);

        if (targetDirection != Vector3.zero)
        {
            //Get input direction relative to camera logic not working
            //Vector3 relativeDirection = GeometryShift.instance.cameraController.gameObject.transform.TransformDirection(targetDirection);
            //Remove Cameras Y from the Direction
            //relativeDirection.Set(relativeDirection.x, 0, relativeDirection.z);
            //Set the target Direction
            //targetRotation = Quaternion.LookRotation(relativeDirection, Vector3.up);

            //temporary until above code is fixed
            targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

            //Preform this frames rotation
            
        }

        //Apply the value
        newRotation = Quaternion.Lerp(newRotation, targetRotation, Time.deltaTime * 10);
        transform.rotation = newRotation;
    }


    protected override void ConfigurePhysics()
    {
        rBody.useGravity = false;
        rBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        rBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rBody.interpolation = RigidbodyInterpolation.Interpolate;
        rBody.mass = 1;
    }

    //posible that this is not needed
    //void OnEnable()
    //{
    //    //On first enable this object will be null but 
    //    //we need to be able to run the following code
    //    //if it is to be enabled multiple times
    //    if (rBody != null)
    //    {
    //        ConfigurePhysics();
    //    }

    //}
}
