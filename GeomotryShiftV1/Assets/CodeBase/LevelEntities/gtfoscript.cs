using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gtfoscript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelBase.instance.AcknowledgeLevelCompletion();
        }
    }

}
