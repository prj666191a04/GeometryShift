using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKill : MonoBehaviour
{
   

    private void OnTriggerEnter(trigger collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (trigger.gameObject.CompareTag("Player"))
        {
            Debug.Log("Kill");
            //Removed one of the below upon inset to master
            trigger.gameObject.GetComponent<CStatus>().Damage(9999f);
            trigger.gameObject.GetComponent<CStatus>().Damage(9999f);

            
        }
    }

   
}
