using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CController : MonoBehaviour {

    public float h;
    public float v;
    CMotor motor;
    Rigidbody rBody;

	// Use this for initialization
	void Start () {
        //TODO: motor selector
        motor = this.gameObject.AddComponent<TriMovementA>();
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
	// Update is called once per frame
	void Update () {
        
        h = GSInput.GetHAxis();
        v = GSInput.GetVAxis();
        //TODO manipulate data

        motor.h_ = h;
        motor.v_ = v;
	}

    void ChangeMotor()
    {

    }

}
