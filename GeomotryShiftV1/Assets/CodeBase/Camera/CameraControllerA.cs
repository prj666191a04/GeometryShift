using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Will need a CameraControllerBase class has a parent in the future to account for the posibility of multiple controllers
public class CameraControllerA : MonoBehaviour
{
    public Vector3 offset;
    public Transform target;
    public Vector3 anchorPoint;
    public Vector3 currentPos;
    public Vector3 targetPoistion;

    public Vector3 velocity = Vector3.one;

    //Set to 0 to remove border follow effect
    public float borderDistance = 5f;

    public float smoothSpeed = 0.125f;


    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void SetTarget(GameObject t)
    {
        target = t.transform;
    }

    public void Init()
    {
      anchorPoint = target.position + offset;
      transform.position = anchorPoint;
      currentPos = transform.position;
      targetPoistion = anchorPoint;
    }
    public void Init(Vector3 offsetIn)
    {
        offset = offsetIn;
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            //UpdateTargetInfo();
            //MoveToPosition();
        }
    }

    private void LateUpdate()
    {
        if(target != null)
        {
            UpdateTargetInfo();
            MoveToPosition();
        }
        
    }

    void UpdateTargetInfo()
    {
        anchorPoint = target.position + offset;
       
    }

    void MoveToPosition()
    {
        if (Vector3.Distance(transform.position, anchorPoint) >= borderDistance)
        {
            //currentPos = Vector3.Lerp(transform.position, anchorPoint, 3 * Time.deltaTime);
            currentPos = Vector3.SmoothDamp(transform.position, anchorPoint, ref velocity, 0.1f);
            transform.position = currentPos;
        }
        //transform.position = anchorPoint;

    }

    public void LookAt(GameObject t)
    {
        transform.LookAt(t.transform);
    }
    public void LookAt(Vector3 rotation)
    {
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }
    public void LookAt(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
}
