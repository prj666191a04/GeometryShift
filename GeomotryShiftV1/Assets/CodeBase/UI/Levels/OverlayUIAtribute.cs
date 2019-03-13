//Author Atilla puskas
//Description: base class for attributes of a overlayUI
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OverlayUIAtribute : MonoBehaviour
{
    public LevelOverlayUI owner_;

    public abstract void Play();

}
