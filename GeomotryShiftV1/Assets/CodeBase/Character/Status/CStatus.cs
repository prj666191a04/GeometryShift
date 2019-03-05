using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CStatus : MonoBehaviour
{
    public delegate void DeathDel(int method);
    public static event DeathDel OnPlayerDeath;

    public float value_;
    public int maxValue_;
    public float dspValue_;

    public abstract void Damage(float ammount);

    public abstract void Recover(float ammount);

    public abstract void Initialize(float ammount);

    public abstract void Reset();


    public void Die(int method = 0)
    {
        if(OnPlayerDeath != null)
        {
            OnPlayerDeath(method);
        }
    }
    
    private void OnEnable()
    {
        GeometryShift.playerStatus = this;
    }

}
