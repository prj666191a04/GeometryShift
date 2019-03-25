//Author Atilla puskas
//Description: overriden logic specificly for the exit tab of the pause menue

using System.Collections;
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
            SystemSounds.instance.acUI.Play();
            LevelBase.instance.TerminateLevelAttempt();
            GeometryShift.instance.pauseMenue.Hide();
            GeometryShift.instance.pauseMenueActive = false;
            Time.timeScale = 1;
        }
        else
        {
            SystemSounds.instance.acUI.Play();
            LevelLoader.instance.ReturnToMainMenue();
            LevelLoader.instance.AutoSave();
            GeometryShift.instance.StopSessionTimer();
            GeometryShift.instance.pauseMenue.Hide();
            GeometryShift.instance.pauseMenueActive = false;
            Time.timeScale = 1;
            GeometryShift.instance.interactionUI.Hide();
        }
    }
}
