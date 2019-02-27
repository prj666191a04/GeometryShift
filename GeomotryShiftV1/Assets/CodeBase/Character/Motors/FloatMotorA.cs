using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FloatMotorA : CMotor
{
    Transform mapFlow;
    Transform screenPos;
    Camera screenBoundsCam;

    public LayerMask doNotPassThrogh;
    public LayerMask teleportMask;

    public GameObject lookIndicator;

    Quaternion newRotation;
    Quaternion targetRotation;
    Vector3 targetDirection;
    public Vector3 pos;
    public Vector3 screemPos;

    public Vector3 screenBounds;
    public Vector3 mouseWorldPos;
    public Vector3 targetAhead;
    public Vector3 lookPos;



    private void Start()
    {
        
        mapFlow = GameObject.Find("MapFlow").GetComponent<Transform>();
        lookIndicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        lookIndicator.transform.localScale.Set(0.5f, 0.5f, 0.5f);
        lookIndicator.GetComponent<MeshRenderer>().material.color = Color.red;
        Destroy(lookIndicator.GetComponent<SphereCollider>());



        transform.forward = Vector3.up;
        newRotation = transform.rotation;
        targetRotation = transform.rotation;
        doNotPassThrogh = LevelBase.instance.layerSet0;
        teleportMask = LevelBase.instance.layerSet1;

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.scaledPixelWidth, Camera.main.scaledPixelHeight, Camera.main.transform.position.z));

    }

    private void Update()
    {
        RotateToDirection();
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(h_, v_, 0 );
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
        ClampToScreen();
         pos = Camera.main.WorldToViewportPoint(transform.position);
          
        if (pos.x < 0.0) Debug.Log("left");
        if (1.0 < pos.x) Debug.Log("right");
        if (pos.y < 0.0) Debug.Log("below");
        if (1.0 < pos.y) Debug.Log("above");
    }


    void ClampToScreen()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.scaledPixelWidth, Camera.main.scaledPixelHeight, Camera.main.transform.position.z));
        // transform.InverseTransformPoint(screenBounds);
        screenBounds.x = screenBounds.x + mapFlow.transform.position.x * -1;
        screenBounds.y = screenBounds.y + mapFlow.transform.position.y * -1;

        Vector3 cpos = transform.localPosition;
        cpos.x = Mathf.Clamp(cpos.x, (screenBounds.x + 1f), (screenBounds.x) * -1 - 1f);
        cpos.y = Mathf.Clamp(cpos.y, (screenBounds.y + 1f), (screenBounds.y) * -1 - 1f);

        transform.localPosition = cpos;
         

    }

    void RotateToDirection()
    {

        //0 up 0 , 1
        //90 left -1, 0
        //-90 right 1, 0
        //180 down 0 -1
        //Get direction of input



        targetDirection = new Vector3(h_, v_, 0);

        switch (h_)
        {
            case 1:
                break;
        }


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
        //transform.rotation = newRotation;


        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, Camera.main.transform.position.z *-1));

        if(targetDirection != Vector3.zero)
            targetAhead = transform.position + targetDirection *2;
        
        //targetAhead.z = 0;
        Vector3 target = mouseWorldPos;
        target = targetAhead;

        lookIndicator.transform.position = target;

        Vector3 newLook = target - transform.position;
        newLook.z = 0;

        newLook = Vector3.Lerp(lookPos, newLook, Time.deltaTime * 5);
        
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        //rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
        //transform.rotation = rotation;

        transform.rotation = rotation;

        lookPos = newLook;
    }


    protected override void ConfigurePhysics()
    {
        rBody.useGravity = false;
        rBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ ;
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
