using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Default healthbased status script
public class CStatusA : CStatus
{
    AudioClip playerHit;
    
    private float lerpSpeed = 2f;
    private float minLerpSpeed = 1f;
    private float maxLerpSpeed = 6f;

    private float secondsSinceLastTookDamage = 1;

    public Material t1;

    // Start is called before the first frame update
    void Start()
    {
        value_ = 10;
        playerHit = Resources.Load("Audio/playerHitGood") as AudioClip;
    }

    // Update is called once per frame
    void Update()
    {
        secondsSinceLastTookDamage += Time.deltaTime;
        if (secondsSinceLastTookDamage > 0.15)
        {
            GetComponent<MeshRenderer>().material = Resources.Load("Materials/LightGray") as Material;
        }
        LerpDsp();
    }

    public override void Damage(float ammount)
    {
        AudioSource.PlayClipAtPoint(playerHit, Vector3.zero);
        
        if (value_ > 0)
        {
            value_ -= ammount;
            GetComponent<MeshRenderer>().material = Resources.Load("Materials/PlayerInjuredColor") as Material;
            secondsSinceLastTookDamage = 0;
        }
        else
        {
            GetComponent<MeshRenderer>().material = Resources.Load("Materials/PlayerInjuredColor") as Material;
            secondsSinceLastTookDamage = 0;
            value_ = 0;
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
