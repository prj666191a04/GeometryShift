//Atilla Puskas
//Disables a TrapRoomA script on interaction

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceTerminal : CInteractable
{

    public Color armedColor;
    public Color disarmedColor;

    public MeshRenderer crystalRend;
    Material crystalMat;

    public delegate void TerminalDel(int id);
    public static event TerminalDel OnTerminalMessage;

    public int targetID;
    public bool toggleMode = false;
    bool activated = false;

    string orginalText;
    public string alturnateText;
    public override void Respond()
    {
        GeometryShift.instance.interactionUI.Apear(interactText_);
    }
    public override void Trigger()
    {
        if(!activated || toggleMode)
        {
            if(OnTerminalMessage != null)
            {
                OnTerminalMessage(targetID);
                activated = !activated;
                SetText();
                GeometryShift.instance.interactionUI.Apear(interactText_);
            }
        }
        else
        {
            SystemSounds.instance.UIError();
        }
    }
    private void SetText()
    {
        if(activated)
        {
            interactText_ = alturnateText;
            crystalMat.color = disarmedColor;
        }
        else
        {
            interactText_ = orginalText;
            crystalMat.color = armedColor;
        }
    }
    private void OnEnable()
    {
        LevelBase.OnLevelReset += ResetTerminal;
    }
    private void OnDisable()
    {
        LevelBase.OnLevelReset -= ResetTerminal;
    }
    void ResetTerminal()
    {
        activated = false;
        interactText_ = orginalText;
    }
    // Start is called before the first frame update
    void Start()
    {
        orginalText = interactText_;
        crystalMat = crystalRend.materials[0];
    }
}

