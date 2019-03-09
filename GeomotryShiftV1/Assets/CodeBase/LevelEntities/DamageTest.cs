//Author Atilla puskas
//Description: used to test the damage system


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CStatus>().Damage(1f);
        }
    }
}
