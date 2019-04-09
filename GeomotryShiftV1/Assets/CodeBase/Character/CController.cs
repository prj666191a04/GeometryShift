//Author Atilla puskas
//Description: The main controling object for the character, sends information to the motors


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CController : MonoBehaviour {

    MeshRenderer meshRenderer;
    BoxCollider mainColider;
    public ParticleSystem deathPs;
    public ParticleSystem hitPs;
    public ParticleSystem dashPs;
    public ParticleSystem recoverPs;
    public float h;
    public float v;
    public CMotor motor;
    public Rigidbody rBody;
    public CMotor[] motorPool;

    public CStatus activeStatus;

    public bool autoInit = false;
    public bool movementDisabled = false;
    private bool isDead = false;
    private bool disabled = false;

    public bool IsDisabled()
    {
        return disabled;
    }

    private void OnEnable()
    {
        SubEvents();
    }
    private void OnDisable()
    {
        UnsubEvents();
    }
    void SubEvents()
    {
        CStatus.OnPlayerHit += Hit;
        CStatus.OnPlayerDeath += Die;
        LevelBase.OnLevelRessultScreen += DisableMovement;
    }
    void UnsubEvents()
    {
        CStatus.OnPlayerDeath -= Die;
        CStatus.OnPlayerHit -= Hit;
        LevelBase.OnLevelRessultScreen -= DisableMovement;
    }
    //Can be removed later for testing only
    void DefaultInitialization()
    {
        motorPool = new CMotor[1];
        motor = this.gameObject.AddComponent<TriMovementA>();
        motorPool[0] = motor;
        if (this.gameObject.GetComponent<Rigidbody>())
        {
            rBody = this.gameObject.GetComponent<Rigidbody>();
        }
        else
        {
            this.rBody = this.gameObject.AddComponent<Rigidbody>();
            this.rBody.useGravity = true;
        }
        motor.SetPhysics(rBody);
        motor.controller_ = this;
    }
    //used to set the first motor for the level
    public void AssignMotor(CMotor m)
    {
        motor = m;
        motor.SetPhysics(rBody);
    }
    //used to change motor in level
    public void ChangeMotor(int index)
    {
        if(index <= motorPool.Length - 1 && index >= 0 && motorPool[index] != null)
        {
            motor.enabled = false;
            motor = motorPool[index];
            motor.SetPhysics(rBody);
            motor.controller_ = this;
            motor.enabled = true;
        }
        else
        {
            Debug.LogWarning("Motor not changed invalid index was provided to ChangeMotor Please compare the indexes of the chosen CConfig with the index passed");
        }

    }

	// Use this for initialization
	void Start () {
        if(autoInit)
        {
            DefaultInitialization();
        }
        meshRenderer = GetComponent<MeshRenderer>();
        mainColider = GetComponent<BoxCollider>();
        motor.controller_ = this;
       
	}
	// Update is called once per frame
	void Update () {
        
        h = GSInput.GetHAxis();
        v = GSInput.GetVAxis();
        if (!isDead)
        {
            motor.h_ = h;
            motor.v_ = v;
        }
        else
        {
            motor.h_ = 0;
            motor.v_ = 0;
        }
	}

    
    public void DisableMovement()
    {
        if (!disabled)
        {
            Debug.Log("movementEnabled");
            motor.enabled = false;
            Destroy(rBody);
            mainColider.enabled = false;
            disabled = true;
        }
    }
   public void EnableMovement()
    {
        if(disabled)
        {
            Debug.Log("movementDisabled");
            rBody = gameObject.AddComponent<Rigidbody>();
            motor.enabled = true;
            motor.SetPhysics(rBody);
            mainColider.enabled = true;
            rBody.WakeUp();
            disabled = false;
        }
    }
    void Die(int method = 0)
    {
        if (!isDead)
        {
            SystemSounds.instance.EffectsHit();
            SystemSounds.instance.EffectDeath();
            isDead = true;
            meshRenderer.enabled = false;
            deathPs.Emit(100);
            DisableMovement();
        }
    }
    void Hit()
    {
        hitPs.Emit(5);
        SystemSounds.instance.EffectsHit();
    }

    public void DashEffect()
    {
        dashPs.Emit(30);
    }

    public void Respawn(Vector3 postion, bool worldspace = false)
    {
        isDead = false;
        meshRenderer.enabled = true;
        if(worldspace)
        {
            transform.position = postion;
        }
        else
        {
            transform.localPosition = postion;
        }
        EnableMovement();
    }

}
