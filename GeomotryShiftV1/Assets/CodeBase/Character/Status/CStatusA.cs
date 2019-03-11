//Author Atilla puskas
//Description: a basic implementation of a status

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Default healthbased status script
public class CStatusA : CStatus
{
    private float lerpSpeed = 2f;
    private float minLerpSpeed = 1f;
    private float maxLerpSpeed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        value_ = 3;
    }

    // Update is called once per frame
    void Update()
    {
        LerpDsp();
    }

    public override void AbsoluteDamage(float ammount)
    {
        Debug.Log("sbs damage entered");
        if (value_ - ammount > 0)
        {
            value_ -= ammount;
            HitAnimation();
            StartCoroutine(ActivateIFrames());
        }
        else
        {
            value_ = 0;
            Die();
            Reset();
        }
    }

    public override void Damage(float ammount)
    {
        if (!iFrame_)
        {
            if (value_ - ammount > 0)
            {
                value_ -= ammount;
                HitAnimation();
                StartCoroutine(ActivateIFrames());
            }
            else
            {
                value_ = 0;
                Die();
                Reset();
            }
        }
    }
    public override void Recover(float ammount)
    {
        throw new System.NotImplementedException();
    }
    public override void Initialize(float ammount)
    {
        throw new System.NotImplementedException();
    }
    public override void Reset()
    {
        value_ = 3;
        dspValue_ = 3;
    }

    private void LerpDsp()
    {
        if (value_ != dspValue_)
        {
            float decentValue = dspValue_ - Time.deltaTime * lerpSpeed;
            if (dspValue_ < value_)
            {
                dspValue_ = value_;
            }
            else
            {
                dspValue_ = decentValue;
            }


        }
    }






}
