using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticKillCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GeometryShift.playerStatus.AbsoluteDamage(9000);
            Debug.Log("damage");
        }
        else if(other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
