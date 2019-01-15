using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyController : MonoBehaviour
{
    public Rigidbody theRB;
    public float speedMultiplier = 6;
    public float jumpForce = 8f;
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 myVector = new Vector3(0, 0, 0);
        myVector.x = Input.GetAxis("Horizontal");
        myVector.y = theRB.velocity.y;
        myVector.z = Input.GetAxis("Vertical");
        myVector.x *= speedMultiplier;
        myVector.z *= speedMultiplier;
        theRB.velocity = myVector;

        if (Input.GetButtonDown("Jump"))
        {
            theRB.velocity = new Vector3(theRB.velocity.x, jumpForce, theRB.velocity.z);
        }
    }
}
