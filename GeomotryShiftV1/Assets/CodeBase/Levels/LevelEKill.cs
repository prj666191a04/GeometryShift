using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEKill : LevelBase
{
    public Transform cameraPoint;
    public GameObject spawn;

    void OnEnable()
    {
        CStatus.OnPlayerDeath += foo();

    }

    void OnDisable()
    {
        CStatus.OnPlayerDeath -= foo();
    }

    public void foo() {
        StartCoroutine(playerRespawn());

    }


    IEnumerator playerRespawn()
    {
        yield return new WaitForSeconds(3);
        GeometryShift.playerStatus.gameObject.GetComponent<CController>().Respawn(spawn.transform.position, true);
        yield break;
    }
}
