using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author Atilla puskas
//Description: controls for an overlay ui to be used in a level

public class LevelOverlayUI : MonoBehaviour
{
    public delegate void OverlayResponse();
    //Called when the intro has finished its animation
    public static event OverlayResponse OnIntroFinished;
    //Called when the player has inputed that they want to retry the level
    public static event OverlayResponse OnRetryRequested;
    //Called when the player has indicated they want to quit the level
    public static event OverlayResponse OnLevelQuit;
    


    public GameObject introPrefab;
    public GameObject RetryScreenPrefab;

    GameObject introInstance;
    OverlayUIAtribute introScript;

    GameObject retryScreenInstance;
    OverlayUIAtribute retryScript;

    private void Start()
    {
        introInstance = GameObject.Instantiate(introPrefab, this.transform);
        introScript = introInstance.GetComponent<OverlayUIAtribute>();
        introScript.owner_ = this;
        retryScreenInstance = GameObject.Instantiate(RetryScreenPrefab, this.transform);
        retryScript = retryScreenInstance.GetComponent<OverlayUIAtribute>();
        retryScript.owner_ = this;
        retryScreenInstance.SetActive(false);
       
        
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
    public void ShowRsltScreen()
    {
        
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
