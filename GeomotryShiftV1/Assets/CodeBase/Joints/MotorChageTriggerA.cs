//Author Atilla puskas
//Description: swaps the currently active motor on contact

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorChageTriggerA : MonoBehaviour
{
    public int motorIndex;


    public bool forceCPrespective = false;
    public bool force3dTimedPrespective = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<CController>().ChangeMotor(motorIndex);
            if(forceCPrespective)
            {
                ForceCPrespective(other);
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
