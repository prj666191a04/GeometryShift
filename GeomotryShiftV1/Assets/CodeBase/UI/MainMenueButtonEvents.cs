//Author Atilla Puskas
// desc Responsds to events using and attached EventTrigger component


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenueButtonEvents : MonoBehaviour
{
   public void Rollover()
   {
        SystemSounds.instance.UIRollOver();
   }
    public void Click()
    {
        SystemSounds.instance.UIClick();
    }
}
