using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FloatMotorA : CMotor
{

    public LayerMask doNotPassThrogh;
    public LayerMask teleportMask;

    public GameObject asda;

    Quaternion newRotation;
    Quaternion targetRotation;
    Vector3 targetDirection;
    public Vector3 pos;
    public Vector3 screemPos;

    public Vector2 screenBounds;

    private void Start()
    {
        newRotation = transform.rotation;
        targetRotation = transform.rotation;
        doNotPassThrogh = LevelBase.instance.layerSet0;
        teleportMask = LevelBase.instance.layerSet1;

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        
    }

    private void Update()
    {
        RotateToDirection();
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(h_, 0, v_);
        if (moveDirection != null)
        {
            RaycastHit hitInfo;
            Ray ray = new Ray();
            ray.direction = moveDirection;
            ray.origin = transform.position;
            if (Physics.Raycast(ray, out hitInfo, 0.3f + 0.5f, doNotPassThrogh))
            {
                rBody.MovePosition(transform.position + moveDirection * (hitInfo.distance - 0.5f));
            }
            else
            {
                rBody.MovePosition(transform.position + moveDirection * 0.3f);
            }
        }
       
    }

    private void LateUpdate()
    {
        //ClampToScreen();
         pos = Camera.main.WorldToViewportPoint(transform.position);
          
        if (pos.x < 0.0) Debug.Log("left");
        if (1.0 < pos.x) Debug.Log("right");
        if (pos.y < 0.0) Debug.Log("below");
        if (1.0 < pos.y) Debug.Log("above");
    }


    void ClampToScreen()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, screenBounds.x, screenBounds.x * -1);
        //clampedPosition.y = Mathf.Clamp(clampedPosition.y, screenBounds.y, screenBounds.y * -1);
        transform.position = clampedPosition;
    }

    void RotateToDirection()
    {

        //Get direction of input
        targetDirection = new Vector3(h_, 0, v_);

        if (targetDirection != Vector3.zero)
        {
            //Get input direction relative to camera logic not working
            //Vector3 relativeDirection = GeometryShift.instance.cameraController.gameObject.transform.TransformDirection(targetDirection);
            //Remove Cameras Y from the Direction
            //relativeDirection.Set(relativeDirection.x, 0, relativeDirection.z);
            //Set the target Direction
            //targetRotation = Quaternion.LookRotation(relativeDirection, Vector3.up);

            //temporary until above code is fixed
            targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

            //Preform this frames rotation
            
        }

        //Apply the value
        newRotation = Quaternion.Lerp(newRotation, targetRotation, Time.deltaTime * 10);
        transform.rotation = newRotation;
    }


    protected override void ConfigurePhysics()
    {
        rBody.useGravity = false;
        rBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
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
