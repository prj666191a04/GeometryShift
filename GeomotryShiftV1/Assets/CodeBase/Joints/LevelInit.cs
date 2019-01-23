using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Its starting to look like this class might not be needed. currently a class of type cconfig could be used directly
/// but we are keeping it for the time being for orginization It will be removed once the system is compleete if there is still no need for it.
/// </summary>
public class LevelInit : MonoBehaviour
{

    public GameObject playerPrefab;
    public Transform spawnPoint; //this will be replaced with save data later for open world map
    public GameObject parentObject;

    
    public CConfig cconfig;

    // Start is called before the first frame update
    void Start()
    {
        // test = new CConfig();
        //test.playerPrefab = playerPrefab;
        //test.spawnPoint = spawnPoint;
        //test.parentObject = parentObject;
        //test.SetupCharacter();

        cconfig.SetupCharacter(playerPrefab, spawnPoint, parentObject);

        //SetupCharacter();
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
