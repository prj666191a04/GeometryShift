//Author Allan

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalLevel1 : LevelBase
{


    public Transform cameraPoint;
    public GameObject spawn;
    //public LevelOverlayUI theLevelUI; doesn't work

    void OnEnable()
    {
        CStatus.OnPlayerDeath += foo;

    }

    void OnDisable()
    {
        CStatus.OnPlayerDeath -= foo;
    }

    private void foo(int i = 0)
    {
        //Debug.Log("Enter Foo");
        StartCoroutine(playerRespawn());
    }


    IEnumerator playerRespawn()
    {
        //Debug.Log("Enter Player Respawn");
        yield return new WaitForSeconds(0.1f);
        //LevelLoader.instance.LoadLevel(itself);
        //Destroy(gameObject);
        yield break;
    }
}