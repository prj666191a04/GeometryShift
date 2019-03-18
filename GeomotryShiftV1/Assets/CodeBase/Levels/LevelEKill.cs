//Author Harly Sims, Edited by Atilla Puskas
//controlling script for LevelE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEKill : LevelBase
{
    public Transform cameraPoint;
    public GameObject spawn;
    public LevelOverlayUI levelUi;
    


    private void ResetLevel(int method = 0)
    {
        levelUi.ShowRetryScreen();
    }

    void OnEnable()
    {
        CStatus.OnPlayerDeath += ResetLevel;
        LevelOverlayUI.OnResultScreenFinished += base.AcknowledgeLevelCompletion;
        LevelOverlayUI.OnRetryRequested += foo;
        LevelOverlayUI.OnLevelQuit += TerminateLevelAttempt;
    }

    void OnDisable()
    {
        CStatus.OnPlayerDeath -= ResetLevel;
        LevelOverlayUI.OnResultScreenFinished -= base.AcknowledgeLevelCompletion;
        LevelOverlayUI.OnRetryRequested -= foo;
        LevelOverlayUI.OnLevelQuit -= TerminateLevelAttempt;
    }

    // Note: for Harley sims, I would have expected for you to at rename sample functions I give you to have meaningful names once in production.
    private void foo() {
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
    public override void AcknowledgeLevelCompletion(int code = 0)
    {
        levelUi.ShowRsltScreen("", code);
        LevelRessultScreenSendMessage();
    }
}
