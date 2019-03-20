//Author Atilla puskas
//Description: a base class to handle player damage and other penalties

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CStatus : MonoBehaviour
{
    public delegate void DeathDel(int method);
    public delegate void HitDel();
    public static event DeathDel OnPlayerDeath;
    public static event HitDel OnPlayerHit;



    public float value_;
    public int maxValue_;
    public float dspValue_;

    protected float iFrameTime_ = 0.5f;
    protected bool iFrame_ = false;

    public abstract void Damage(float ammount);

    public abstract void AbsoluteDamage(float ammount);

    public abstract void Recover(float ammount);

    public abstract void Initialize(float ammount);

    public abstract void RecoverItem();

    public abstract void Reset();

    protected IEnumerator ActivateIFrames()
    {
        iFrame_ = true;
        yield return new WaitForSeconds(iFrameTime_);
        iFrame_ = false;
    }

    protected void Die(int method = 0)
    {
        if(OnPlayerDeath != null)
        {
            OnPlayerDeath(method);
        }
    }

   protected void HitAnimation()
    {
        if(OnPlayerHit != null)
        {
            OnPlayerHit();
        }
    }
    
    private void OnEnable()
    {
        GeometryShift.playerStatus = this;
    }

}
