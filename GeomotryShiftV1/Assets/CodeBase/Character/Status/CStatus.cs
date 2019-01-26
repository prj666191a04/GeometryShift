using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CStatus : MonoBehaviour
{
   public float value_;
   public float dspValue_;

    public abstract void Damage(float ammount);

    public abstract void Recover(float ammount);

    public abstract void Initialize(float ammount);

}
