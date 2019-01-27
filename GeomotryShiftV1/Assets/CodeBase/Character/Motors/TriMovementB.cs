using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script used to test motor change
public class TriMovementB : CMotor
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rBody.MovePosition((transform.forward * v_) * (Time.deltaTime * 2));
        //Quaternion TargetRotation = transform.rotation.;
    }
}
