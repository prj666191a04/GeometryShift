//Author Atilla puskas
//Description: swaps the currently active motor on contact

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorChageTriggerA : MonoBehaviour
{
    public int motorIndex;

    //This method of setting bools is sloppy and dangoruse however due to close deadlines 
    //we will have to carfuly proceed with this method making sure to not incorectly check the values in the inspector.
    //The proper solution would be to right more then one script for this functinality

    //This value should never be true if this specific instance is not applying a BiMovementC motor
    public bool inverseBiMovement = false;

    //Only one of the following should be true at any given time
    public bool forceCPrespective = false; //This must be true if the motor is Bi movmentC
    public bool force3dTimedPrespective = false;//This must be true if the motor is timed3D


    //Failing to heed the above warnings may ressult in large errors.

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<CController>().ChangeMotor(motorIndex);
            if(forceCPrespective)
            {
                ForceCPrespective(other);
                other.GetComponent<BiMovementC>().inversed = inverseBiMovement;
            }
            if(force3dTimedPrespective)
            {
                GeometryShift.instance.cameraController.Init(new Vector3(0f, 13f, -6f));
                GeometryShift.instance.cameraController.LookAt(new Vector3(68.931f, 0f, 0f));
                GeometryShift.instance.cameraController.borderDistance = 4f;
            }

        }
    }

    void ForceCPrespective(Collider other)
    {
        GeometryShift.instance.cameraController.Init(new Vector3(0f, 0f, -15f));
        GeometryShift.instance.cameraController.LookAt(new Vector3(1f, 1f, -.22f));
        GeometryShift.instance.cameraController.borderDistance = 0;
    }

   
}
