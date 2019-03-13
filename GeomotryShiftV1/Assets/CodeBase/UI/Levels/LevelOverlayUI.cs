using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author Atilla puskas
//Description: controls for an overlay ui to be used in a level

public class LevelOverlayUI : MonoBehaviour
{
    public delegate void OverlayResponse();
    public static event OverlayResponse OnIntroFinished;
    public static event OverlayResponse OnRetryRequested;
    public static event OverlayResponse OnLevelQuit;
    


    public GameObject introPrefab;
    public GameObject rsltScreenPrefab;

    GameObject introInstance;
    OverlayUIAtribute introScript;

    GameObject rsltScreenInstance;
    OverlayUIAtribute rsltScript;

    private void Start()
    {
        introInstance = GameObject.Instantiate(introPrefab, this.transform);
        introScript = introInstance.GetComponent<OverlayUIAtribute>();
        introScript.owner_ = this;
        rsltScreenInstance = GameObject.Instantiate(rsltScreenPrefab);
        rsltScript = rsltScreenInstance.GetComponent<OverlayUIAtribute>();
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
    public void ShowRsltScreen()
    {
        rsltScript.Play();
    }

    public void ShowRetryScreen()
    {

    }

    public void WarnPlayer()
    {

    }
    public void MessageQueue(string message)
    {

    }
 
}
