﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PMTabExit : PMTab
{
    public TMP_Text text;


    private void OnEnable()
    {
        if(GeometryShift.GetSystemState() == GeometryShift.SystemState.InLevel)
        {
            text.text = "Quit level";
            text.fontSize = 35;
        }
        else
        {
            text.text = "Return to main menue";
            text.fontSize = 21;
        }
    }
    private void OnDisable()
    {
        
    }

    // Start is called before the first frame update
    public override void Select()
    {
        if(GeometryShift.GetSystemState() == GeometryShift.SystemState.InLevel)
        {
            LevelBase.instance.TerminateLevelAttempt();
            GeometryShift.instance.pauseMenue.Hide();
            Time.timeScale = 1;
        }
        else
        {
            Debug.Log("else");
            LevelLoader.instance.ReturnToMainMenue();
            GeometryShift.instance.pauseMenue.Hide();
            Time.timeScale = 1;  
        }
    }
}
