//Author Atilla puskas
//Description: a motor that clamps the charachter to the screen for a specific kind of gameplay expeeriance


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FloatMotorA : CMotor
{
    CameraControllerA cameraController;

    Transform mapFlow;
    Transform screenPos;
    Camera screenBoundsCam;

    public LayerMask doNotPassThrogh;
    public LayerMask teleportMask;


    Quaternion targetRotation;
    Vector3 targetDirection;
    public Vector3 pos;
    public Vector3 screemPos;

    public Vector3 cpos;
    public Vector3 screenBounds;
    public Vector3 mouseWorldPos;
    public Vector3 targetAhead;
    public Vector3 lookPos;

    public bool action1Key = false;


    private void Start()
    {
        controller_ = GetComponent<CController>();
        mapFlow = GameObject.Find("MapFlow").GetComponent<Transform>();
        screenBoundsCam = Camera.main;
        cameraController = Camera.main.GetComponent<CameraControllerA>();
        transform.forward = Vector3.up;
        targetRotation = transform.rotation;
        doNotPassThrogh = LevelBase.instance.layerSet0;
        teleportMask = LevelBase.instance.layerSet1;
        screenBounds = screenBoundsCam.ScreenToWorldPoint(new Vector3(screenBoundsCam.scaledPixelWidth, screenBoundsCam.scaledPixelHeight, cameraController.offset.z));

    }

    private void Update()
    {
        if (!controller_.IsDisabled())
        {
            CustomInput();
            RotateToDirection();
        }
        if (action1Key)
        {
            Vector3 dir = new Vector3(h_, v_, 0);
            if (dir == Vector3.zero)
                dir = transform.forward;

            controller_.DashEffect();
            transform.position = transform.position + dir * 4;
        }
        //MoveCharacter();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }


    private void MoveCharacter()
    {
        Vector3 moveDirection = new Vector3(h_, v_, 0);
        if (moveDirection != null)
        {
            RaycastHit hitInfo;
            Ray ray = new Ray();
            ray.direction = moveDirection;
            ray.origin = transform.position;
            if (Physics.Raycast(ray, out hitInfo, 0.3f + 0.15f, doNotPassThrogh))
            {
                rBody.MovePosition(transform.position + moveDirection * (hitInfo.distance - 0.5f));
            }
            else
            {
                rBody.MovePosition(transform.position + moveDirection * 20f * Time.deltaTime);
            }

        }
    }

    private void LateUpdate()
    {
        ClampToScreen();
        pos = Camera.main.WorldToViewportPoint(transform.position);
          
        if (pos.x < 0.0) Debug.Log("left");
        if (1.0 < pos.x) Debug.Log("right");
        if (pos.y < 0.0) Debug.Log("below");
        if (1.0 < pos.y) Debug.Log("above");
    }


    void ClampToScreen()
    {
        
        screenBounds = screenBoundsCam.ScreenToWorldPoint(new Vector3(screenBoundsCam.scaledPixelWidth, screenBoundsCam.scaledPixelHeight, cameraController.offset.z));
        screenBounds.x = screenBounds.x + mapFlow.transform.position.x * -1;
        screenBounds.y = screenBounds.y + mapFlow.transform.position.y * -1;

        cpos = transform.localPosition;
        cpos.x = Mathf.Clamp(cpos.x, (screenBounds.x + 1f), (screenBounds.x) * -1 - 1f);
        cpos.y = Mathf.Clamp(cpos.y, (screenBounds.y + 1f), (screenBounds.y) * -1 - 1f);
        cpos.z = 0;

        if (transform.localPosition != cpos)
        {
            transform.localPosition = cpos;
        }

    }

    void RotateToDirection()
    {
        
        targetDirection = new Vector3(h_, v_, 0);
        
        mouseWorldPos = screenBoundsCam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, cameraController.offset.z * -1));

        Vector3 target = mouseWorldPos;

        Vector3 heading = target - transform.position;

        if (targetDirection != Vector3.zero)
        {
            //Set the target Direction
            targetRotation = Quaternion.LookRotation(targetDirection, transform.up);

        }
        //Preform this frames rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10);



    }

    public override void CustomInput()
    {
        action1Key = Input.GetKeyDown(KeyCode.Space);    
    }

    protected override void ConfigurePhysics()
    {
        rBody.useGravity = false;
        rBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;
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
