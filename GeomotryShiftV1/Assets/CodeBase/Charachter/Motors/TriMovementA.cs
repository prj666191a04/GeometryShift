using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriMovementA : CMotor
{
    public float speedMultiplier = 6;
    public float jumpForce = 8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 myVector = new Vector3(0, 0, 0);
        myVector.x = h_;
        myVector.y = rBody.velocity.y;
        myVector.z = v_;
        myVector.x *= speedMultiplier;
        myVector.z *= speedMultiplier;
        this.rBody.velocity = myVector;

    }
}
