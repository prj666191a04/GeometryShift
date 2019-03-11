//Author Atilla puskas
//Description: base class for interactable objects

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CInteractable : MonoBehaviour
{
    public abstract void Trigger();
    public abstract void Respond();
    public string interactText_;

}
