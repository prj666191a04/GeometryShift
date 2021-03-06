﻿//Author Atilla puskas
//Description: Controller for the game camera

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

    private void LateUpdate()
    {
        if(target != null)
        {
            UpdateTargetInfo2();
            MoveToPosition2();
            
        }
        
    }

    void UpdateTargetInfo2()
    {
        anchorPoint = target.position + offset;
        Vector3 xVector = new Vector3(transform.position.x, 0, 0);
        Vector3 zVector = new Vector3(0, 0, transform.position.z);
        Vector3 xAnchor = new Vector3(anchorPoint.x, 0, 0);
        Vector3 zAnchor = new Vector3(0, 0, anchorPoint.z);
        float xDist = Vector3.Distance(xVector, xAnchor);
        float zDist = Vector3.Distance(zVector, zAnchor);

        currentPos.y = anchorPoint.y;

        if(xDist > borderDistance)
        {
            //currentPos.x = Mathf.Lerp(currentPos.x, anchorPoint.z, 3 * Time.deltaTime);
            currentPos.x = anchorPoint.x;
            if(transform.position.x > anchorPoint.x)
            {
                currentPos.x += borderDistance;
            }
            if(transform.position.x < anchorPoint.x)
            {
                currentPos.x -= borderDistance;
            }
        }
        if(zDist > borderDistance)
        {
            //currentPos.z = Mathf.Lerp(currentPos.z, anchorPoint.z, 3 * Time.deltaTime);
            currentPos.z = anchorPoint.z;
            if (transform.position.z > anchorPoint.z)
            {
                currentPos.z += borderDistance;
            }
            if (transform.position.z < anchorPoint.z)
            {
                currentPos.z -= borderDistance;
            }

        }

    }

    void MoveToPosition2()
    {
        transform.position = Vector3.SmoothDamp(transform.position, currentPos, ref velocity, 0f);
    }


    //void UpdateTargetInfo()
    //{
    //    anchorPoint = target.position + offset;
    //    if (Vector3.Distance(transform.position, anchorPoint) >= borderDistance)
    //    {
    //        targetPoistion = anchorPoint;
    //        if(transform.position.x > targetPoistion.x)
    //        {
    //            targetPoistion.x += borderDistance;
    //        }
    //        if(transform.position.z > targetPoistion.z)
    //        {
    //            targetPoistion.y += borderDistance;
    //        }
    //        if (transform.position.x < targetPoistion.x)
    //        {
    //            targetPoistion.x -= borderDistance;
    //        }
    //        if (transform.position.z < targetPoistion.z)
    //        {
    //            targetPoistion.z -= borderDistance;
    //        }
    //    }
    //}

    //void MoveToPosition()
    //{
    //    //if (Vector3.Distance(transform.position, anchorPoint) >= borderDistance)
    //    //{
    //        //currentPos = Vector3.Lerp(transform.position, anchorPoint, 3 * Time.deltaTime);
    //        currentPos = Vector3.SmoothDamp(transform.position, targetPoistion, ref velocity, 0.1f);
    //        transform.position = currentPos;
    //    //}
    //    //transform.position = anchorPoint;

    //}

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
