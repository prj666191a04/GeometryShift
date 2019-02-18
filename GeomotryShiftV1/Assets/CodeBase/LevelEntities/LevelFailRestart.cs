using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFailRestart : MonoBehaviour
{

    //public GameObject currentLevel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject spawn = GameObject.FindGameObjectWithTag("Entry");
            player.transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y, spawn.transform.position.z);


        }
    }


}
