//Author Atilla Puskas
//Decription meant to be used in combination with a room key

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery : MonoBehaviour
{
    public bool used = false;
    MeshRenderer rend;
    public float recoverAmmount;

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        LevelBase.OnLevelReset += ResetRecovery;
    }
    private void OnDisable()
    {
        LevelBase.OnLevelReset -= ResetRecovery;
    }
    private void ResetRecovery()
    {
        used = false;
        rend.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !used)
        {
            used = true;
            GeometryShift.playerStatus.Recover(recoverAmmount);
        }
    }



}
