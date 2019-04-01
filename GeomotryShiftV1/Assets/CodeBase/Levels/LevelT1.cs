//Authour Atilla Puskas
//Script to control first timed level

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelT1 : LevelBase
{

    public LevelOverlayUI levelUi;

    bool rsltScreen = false;
    private void OnEnable()
    {
        CStatus.OnPlayerDeath += ResetLevel;
        LevelOverlayUI.OnIntroFinished += StartLevel;
        LevelOverlayUI.OnLevelQuit += TerminateLevelAttempt;
        LevelOverlayUI.OnRetryRequested += ReplayLevel;
        LevelOverlayUI.OnResultScreenFinished += base.AcknowledgeLevelCompletion;
    }
    private void OnDisable()
    {
        CStatus.OnPlayerDeath -= ResetLevel;
        LevelOverlayUI.OnIntroFinished -= StartLevel;
        LevelOverlayUI.OnLevelQuit -= TerminateLevelAttempt;
        LevelOverlayUI.OnRetryRequested -= ReplayLevel;
        LevelOverlayUI.OnResultScreenFinished -= base.AcknowledgeLevelCompletion;
    }

    private void StartLevel()
    {
        
    }

    private void ResetLevel(int method = 0)
    {
        levelUi.ShowRetryScreen();
    }
    private void ReplayLevel()
    {
        GeometryShift.playerStatus.gameObject.GetComponent<CController>().Respawn(init_.spawnPoint.position, true);
        GeometryShift.playerStatus.Reset();
        LevelResetSendMessage();
    }
    // Start is called before the first frame update
    public override void AcknowledgeLevelCompletion(int code = 0)
    {
        if (rsltScreen)
        {
            base.AcknowledgeLevelCompletion(code);
            rsltScreen = true;
        }
        else
        {
            levelUi.ShowRsltScreen(GenerateRessultString(), 0);
            LevelRessultScreenSendMessage();
            CStatusT status = (CStatusT)GeometryShift.playerStatus;
            status.ready = false;

        }
    }
    private string GenerateRessultString()
    {
        CStatusT status = (CStatusT)GeometryShift.playerStatus;
        float timeScore = status.value_ * 100;
        GeometryShift.AwardRecoveryItem(1);
        return "Score: " + timeScore + System.Environment.NewLine + "Rewards: 1 X Recovery";
        
    }


}
