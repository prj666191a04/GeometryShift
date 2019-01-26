using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CController : MonoBehaviour {

    public float h;
    public float v;
    public CMotor motor;
    public Rigidbody rBody;
    public CMotor[] motorPool;
    public CStatus activeStatus;

    public bool autoInit = false;
    public bool movementDisabled = false;




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

    public void AssignMotor(CMotor m)
    {
        motor = m;
        motor.SetPhysics(rBody);
    }

    public void ChangeMotor(int index)
    {

    }

	// Use this for initialization
	void Start () {
        //TODO: motor selector
        if(autoInit)
        {
            DefaultInitialization();
        }
       
	}
	// Update is called once per frame
	void Update () {
        
        h = GSInput.GetHAxis();
        v = GSInput.GetVAxis();
        //TODO manipulate data

        motor.h_ = h;
        motor.v_ = v;
	}



}
