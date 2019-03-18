using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStatusT : CStatus
{
    private float lerpSpeed = 2f;
    public float maxTime;
    public float warningThreshHold;
    public float hp = 3;
    public bool ready = true;

    bool warningTriggered = false;
    bool deathTriggered = false;

    TimerCanvas ui;

    private void Start()
    {
        ui = FindObjectOfType<TimerCanvas>();
        value_ = maxTime;
        maxValue_ = (int)maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        LerpDsp();
        if (ready)
        {
            Tick();
        }
    }

    void Tick()
    {
        if(value_ > 0)
            value_ -= Time.deltaTime;

        if (value_ < 0)
        {
            value_ = 0;
            if (!deathTriggered)
            {
                StartCoroutine(DeathTrigger());
            }
        }

        ui.SetText(value_);

        if (!warningTriggered && value_ < warningThreshHold)
        {
            Debug.Log("warn");
            ui.SetWarningColor();
            warningTriggered = true;
        }
        
    }

    IEnumerator DeathTrigger()
    {
        deathTriggered = true;
        ui.SetDangerColor();
        yield return new WaitForSeconds(0.5f);
        ready = false;
        Die();
    }
    IEnumerator ReadyReset()
    {
        ui.SetText(value_);
        yield return new WaitForSeconds(2f);
        ready = true;
    }

    public override void AbsoluteDamage(float ammount)
    {
        if (hp - ammount > 0)
        {
            hp -= ammount;
            HitAnimation();
            StartCoroutine(ActivateIFrames());
        }
        else
        {
            ready = false;
            hp = 0;
            Die();
            //Reset();
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
                //Reset();
            }
        }
    }
    public override void Recover(float ammount)
    {
        value_ += ammount;
        if(value_ > warningThreshHold)
        {
            warningTriggered = false;
            ui.SetNormalColor();
        }
    }
    public override void Initialize(float ammount)
    {
        throw new System.NotImplementedException();
    }
    public override void Reset()
    {
        value_ = maxTime;
        dspValue_ = maxTime;
        warningTriggered = false;
        deathTriggered = false;
        ui.SetNormalColor();
        StartCoroutine(ReadyReset());
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
