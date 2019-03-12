using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEKill : LevelBase
{
    public Transform cameraPoint;
    public GameObject spawn;

    void OnEnable()
    {
        CStatus.OnPlayerDeath += playerRespawn();

    }

    void OnDisable()
    {
        CStatus.OnPlayerDeath -= playerRespawn();
    }

    //add below to a on event function call
    //GeometryShift.playerStatus.gameObject.GetComponent<CController>().Respawn(spawn.transform.position, true);

    IEnumerator playerRespawn()
    {
        yield return new WaitForSeconds(3);
        GeometryShift.playerStatus.gameObject.GetComponent<CController>().Respawn(spawn.transform.position, true);
    }
}
