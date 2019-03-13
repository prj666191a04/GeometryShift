﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEKill : LevelBase
{
    public Transform cameraPoint;
    public GameObject spawn;

    void OnEnable()
    {
        CStatus.OnPlayerDeath += foo;

    }

    void OnDisable()
    {
        CStatus.OnPlayerDeath -= foo;
    }

    private void foo(int i =0) {
        //Debug.Log("Enter Foo");
        StartCoroutine(playerRespawn());
    }


    IEnumerator playerRespawn()
    {
        //Debug.Log("Enter Player Respawn");
        yield return new WaitForSeconds(3);
        GeometryShift.playerStatus.gameObject.GetComponent<CController>().Respawn(spawn.transform.position, true);
        yield break;
    }
}
