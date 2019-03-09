using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author Atilla puskas
//Description: controls for an overlay ui to be used in a level

public class LevelOverlayUI : MonoBehaviour
{
    public delegate void OverlayResponse();
    public static event OverlayResponse OnIntroFinished;


    public GameObject IntroPrefab;

    GameObject introInstance;
    OverlayUIAtribute introScript;

    private void Start()
    {
        introInstance = GameObject.Instantiate(IntroPrefab, this.transform);
        introScript = introInstance.GetComponent<OverlayUIAtribute>();
        introScript.owner_ = this;
        
        
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

    public void WarnPlayer()
    {

    }
    public void MessageQueue(string message)
    {

    }
    public void ShowRsltScreen()
    {

    }
}
