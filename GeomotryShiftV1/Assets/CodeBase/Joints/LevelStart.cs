using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This needs to be made into an abtract class with children
public class LevelStart : MonoBehaviour
{

    public GameObject playerPrefab;
    public Transform spawnPoint; //this will be replaced with save data later for open world map
    public GameObject parentObject;



    // Start is called before the first frame update
    void Start()
    {
        SetupCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupCharacter()
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

        cc.motorPool = new CMotor[1];
        cc.motorPool[0] = player.AddComponent<TriMovementA>();
        cc.AssignMotor(cc.motorPool[0]);
        cc.motor.SetPhysics(cc.rBody);
        Debug.Log("Character Ready");


    }
}
