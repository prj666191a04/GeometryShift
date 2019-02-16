using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CConfig2DForced : CConfig
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
            cc.rBody.useGravity = false;
        }


        cc.motorPool = new CMotor[1];
        cc.motorPool[0] = player.AddComponent<BiMoementForced>();
        cc.AssignMotor(cc.motorPool[0]);
        cc.motor.SetPhysics(cc.rBody);



        player.AddComponent<CStatusA>();
        Debug.Log("Character Ready");

        GeometryShift.instance.cameraController.SetTarget(player);
        GeometryShift.instance.cameraController.Init(new Vector3(player.transform.position.x, player.transform.position.y, -player.transform.position.z));

        GeometryShift.instance.cameraController.offset = (new Vector3(0f, 0f, -20f));
        GeometryShift.instance.cameraController.LookAt(new Vector3(1f, 1f, -.22f));
        //GeometryShift.instance.cameraController.LookAt(player);

    }
}
