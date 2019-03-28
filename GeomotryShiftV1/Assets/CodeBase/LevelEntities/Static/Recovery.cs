using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery : MonoBehaviour
{
    public int recvoerAmmount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GeometryShift.playerStatus.Recover(recvoerAmmount);
        }
    }



}
