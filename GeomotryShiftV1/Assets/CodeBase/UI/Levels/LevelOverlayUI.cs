using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author Atilla puskas
//Description: controls for an overlay ui to be used in a level

public class LevelOverlayUI : MonoBehaviour
{
    public delegate void OverlayResponse();
    public delegate void OverlayEndResponse(int code = 0);


    //Called when the intro has finished its animation
    public static event OverlayResponse OnIntroFinished;
    //Called when the player has inputed that they want to retry the level
    public static event OverlayResponse OnRetryRequested;
    //Called when the player has indicated they want to quit the level
    public static event OverlayResponse OnLevelQuit;
    //Called when the player is done with the ressult Screen;
    public static event OverlayEndResponse OnResultScreenFinished;
    


    public GameObject introPrefab;
    public GameObject retryScreenPrefab;
    public GameObject rsltScreenPrefab;

    GameObject introInstance;
    OverlayUIAtribute introScript;

    GameObject retryScreenInstance;
    OverlayUIAtribute retryScript;

    GameObject rsltScreenInstance;
    LevelRessultScreenBase rsltScript;

    private int levelExitCode = -1;

    private void Awake()
    {
        introInstance = GameObject.Instantiate(introPrefab, this.transform);
        introScript = introInstance.GetComponent<OverlayUIAtribute>();
        introScript.owner_ = this;
        retryScreenInstance = GameObject.Instantiate(retryScreenPrefab, this.transform);
        retryScript = retryScreenInstance.GetComponent<OverlayUIAtribute>();
        retryScript.owner_ = this;
        retryScreenInstance.SetActive(false);
        rsltScreenInstance = GameObject.Instantiate(rsltScreenPrefab, this.transform);
        rsltScript = rsltScreenInstance.GetComponent<LevelRessultScreenBase>();
        rsltScript.owner_ = this;
        rsltScreenInstance.SetActive(false);
       
    }

    public void PlayIntro()
    {
        introInstance.SetActive(true);
        introScript.Play();
    }
    public void EndIntro()
    {
        introInstance.SetActive(false);
        if(OnIntroFinished != null)
        {
            OnIntroFinished();
        }
    }

    //ToDo: tie rewards into ressult screen
    public void ShowRsltScreen(string rsltText, int returnCode)
    {
        rsltScreenInstance.SetActive(true);
        levelExitCode = returnCode;
        rsltScript.Play();
        rsltScript.SetRessults(rsltText);

    }
    public void ConfirmRessults()
    {
        if(OnResultScreenFinished != null)
        {
            rsltScreenInstance.SetActive(false);
            OnResultScreenFinished(levelExitCode);
        }
    }

    public void ShowRetryScreen()
    {
      retryScreenInstance.SetActive(true);
      retryScript.Play();
    }
    public void RetryReply(bool retry)
    {
        if(retry)
        {
            if(OnRetryRequested != null)
            {
                retryScreenInstance.SetActive(false);
                OnRetryRequested();
            }
        }
        else
        {
            if(OnLevelQuit != null)
            {
                retryScreenInstance.SetActive(false);
                OnLevelQuit();
            }
        }
    }
    public void WarnPlayer()
    {

    }
    public void MessageQueue(string message)
    {

    }
 
}
