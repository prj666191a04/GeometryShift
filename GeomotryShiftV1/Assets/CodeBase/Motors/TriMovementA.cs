using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: inconsistamcu meeds to be fixed. Calculate force needs to be compleeted for more responsive control
public class TriMovementA : CMotor
{

    public Vector3 movementVector;
    //minimum required acceleration rate is 0.6f any lower and there is not enough force to move the object
    public float accelerationRate = 0f;
    private const int maxSpeed = 15;
    private const int maxAcceleration = 5;
    private const int maxBreakingForce = 10;

    public Vector3 velocityDirection;
    public Vector3 normalizedMovementDirection;

    public Vector3 addedForce;

    //real speed in meters per seccond
    public float speed = 0;
    //ground speed only for meters per seccond, no verticle velocoty is included
    public float groundSpeed = 0;
    public Vector3 lastPosition;


    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        Vector3 lastGroundPosition = new Vector3(lastPosition.x, 0, lastPosition.z);
        Vector3 currentGroundPosition = new Vector3(transform.position.x, 0, transform.position.z);

        speed = Vector3.Distance(lastPosition, transform.position) / Time.deltaTime;
        groundSpeed = Vector3.Distance(lastGroundPosition, currentGroundPosition) / Time.deltaTime;

        lastPosition = transform.position;
        CalculateAccelerationForce();
    }

    void FixedUpdate()
    {
        //commented code removed beacuse did not work as intended
        float combinedInput = System.Math.Abs(h_) + System.Math.Abs(v_);

        //makes sure the player is not granted extra force for moving in a diaginal direction
        if (combinedInput >= 2)
        {
            h_ *= 0.7f;
            v_ *= 0.7f;
        }

        movementVector = new Vector3(h_, 0, v_);

        if (movementVector != Vector3.zero)
        {
            rBody.AddForce((movementVector * ((accelerationRate * 1000) * rBody.mass)) * Time.fixedDeltaTime);
            
            //this only exists to view the value in the inspector - remove for finished
            addedForce = (movementVector * ((accelerationRate * 1000) * rBody.mass)) * Time.fixedDeltaTime;
            //only exists for viewing in inspector
        }
        else if (groundSpeed > 3)
        {
            rBody.AddForce((-rBody.velocity.normalized * ((5 * 1000) * rBody.mass)) * Time.fixedDeltaTime);
        }
        velocityDirection = rBody.velocity.normalized;
        normalizedMovementDirection = movementVector.normalized;
    }

    void CalculateAccelerationForce()
    {
        accelerationRate = 1;//maxSpeed - metersPerSeccondGroundSpeed;
        //Material mat = transform.gameObject.GetComponent<MeshRenderer>().material;

       // mat.color = Color.white;


    }

    protected override void ConfigurePhysics()
    {
        rBody.useGravity = true;
        rBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        rBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rBody.interpolation = RigidbodyInterpolation.Interpolate;
        rBody.mass = 1;
    }

    //posible that this is not needed
    //void OnEnable()
    //{
    //    //On first enable this object will be null but 
    //    //we need to be able to run the following code
    //    //if it is to be enabled multiple times
    //    if (rBody != null)
    //    {
    //        ConfigurePhysics();
    //    }
        
    //}

    



}
