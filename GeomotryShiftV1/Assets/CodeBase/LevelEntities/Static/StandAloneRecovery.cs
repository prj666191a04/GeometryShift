//Author Atilla Puskas
//Desc A Recovery meant to be used alone wtih no other components attached

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandAloneRecovery : MonoBehaviour
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
        if(other.gameObject.CompareTag("Player") && !used)
        {
            used = true;
            rend.enabled = false;
            GeometryShift.playerStatus.Recover(recoverAmmount);
        }
    }
}
