using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CController : MonoBehaviour {

    MeshRenderer meshRenderer;
    BoxCollider mainColider;
    public ParticleSystem deathPs;
    public float h;
    public float v;
    public CMotor motor;
    public Rigidbody rBody;
    public CMotor[] motorPool;

    //ignore the value for now
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
        CStatus.OnPlayerDeath += Die;
    }
    void UnsubEvents()
    {
        CStatus.OnPlayerDeath -= Die;
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
            motor.enabled = true;
        }
        else
        {
            Debug.LogWarning("Motor not changed invalid index was provided to ChangeMotor Please compare the indexes of the chosen CConfig with the index passed");
        }

    }

	// Use this for initialization
	void Start () {
        //TODO: motor selector
        if(autoInit)
        {
            DefaultInitialization();
        }
        meshRenderer = GetComponent<MeshRenderer>();
        mainColider = GetComponent<BoxCollider>();
       
	}
	// Update is called once per frame
	void Update () {
        
        h = GSInput.GetHAxis();
        v = GSInput.GetVAxis();
        //TODO manipulate data
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


    void DisableMovement()
    {
        if (!disabled)
        {
            mainColider.enabled = false;
            rBody.Sleep();
            disabled = true;
        }
    }
    void EnableMovement()
    {
        if(disabled)
        {
            mainColider.enabled = true;
            rBody.WakeUp();
            disabled = false;
        }
    }
    void Die(int method = 0)
    {
        if (!isDead)
        {
            isDead = true;
            meshRenderer.enabled = false;
            deathPs.Emit(100);
            DisableMovement();
        }
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
