using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gtfoscript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered End level");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Triggered End level with PLAYER");
            LevelBase.instance.AcknowledgeLevelCompletion();
        }
    }

}
