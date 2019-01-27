using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorChageTriggerA : MonoBehaviour
{
    public int motorIndex;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<CController>().ChangeMotor(motorIndex);
        }
    }
}
