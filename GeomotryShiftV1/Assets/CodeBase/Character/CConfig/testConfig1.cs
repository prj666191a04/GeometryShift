using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testConfig1 : CConfig
{

    public override void SetupCharacter(GameObject playerPrefab, Transform spawnPoint, GameObject parentObject)
    {
        GameObject player = GameObject.Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity, parentObject.transform);
        CController cc = player.GetComponent<CController>();
        //in future will call a configure script for next step this is a hard coded and temporary solution

        if (player.GetComponent<Rigidbody>())
        {
            cc.rBody = player.GetComponent<Rigidbody>();
        }
        else
        {
            cc.rBody = player.AddComponent<Rigidbody>();
            cc.rBody.useGravity = true;
        }

        
        cc.motorPool = new CMotor[2];
        cc.motorPool[0] = player.AddComponent<TriMovementA>();
        cc.AssignMotor(cc.motorPool[0]);
        cc.motor.SetPhysics(cc.rBody);

        cc.motorPool[1] = player.AddComponent<TriMovementB>();
        cc.motorPool[1].enabled = false;

        player.AddComponent<CStatusA>();
        Debug.Log("Character Ready");
        //temporary
        //PlayerReady(player);

        GeometryShift.instance.cameraController.SetTarget(player);
        GeometryShift.instance.cameraController.Init(new Vector3(0f, 45f, -20f));
        

    }



}
