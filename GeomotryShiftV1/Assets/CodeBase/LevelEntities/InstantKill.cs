using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKill : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Kill");
            collision.gameObject.GetComponent<CStatus>().Damage(9999f);
            collision.gameObject.GetComponent<CStatus>().Damage(9999f);
        }
    }
}
