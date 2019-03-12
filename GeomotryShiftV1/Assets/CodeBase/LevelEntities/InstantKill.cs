using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKill : MonoBehaviour
{
   

    private void OnTriggerEnter(collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Kill");
            //Removed one of the below upon inset to master
            collision.gameObject.GetComponent<CStatus>().Damage(9999f);
            collision.gameObject.GetComponent<CStatus>().Damage(9999f);

            
        }
    }

   
}
