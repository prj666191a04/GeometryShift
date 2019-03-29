//Author Atilla Puskas
//desc Toggles the activity of a group of mono behavoirs.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBToggle : MonoBehaviour
{
    public int myID;
    public List<MonoBehaviour> mono;
    bool active = true;

    private void OnEnable()
    {
        LevelBase.OnLevelReset += ResetToggle;
        DefenceTerminal.OnTerminalMessage += Toggle;
    }
    private void OnDisable()
    {
        LevelBase.OnLevelReset -= ResetToggle;
        DefenceTerminal.OnTerminalMessage -= Toggle;
    }
    void Toggle(int id)
    {
        active = !active;
        SetState(active);
    }

    void SetState(bool state)
    {
        foreach (MonoBehaviour m in mono)
        {
            m.enabled = state;
        }
    }

    void ResetToggle()
    {
        active = true;
        SetState(true);
    }

}
