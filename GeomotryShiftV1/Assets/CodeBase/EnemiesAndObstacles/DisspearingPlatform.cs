using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisspearingPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("touched player ");
            other.gameObject.GetComponent<CStatus>().Damage(1.2f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("stopped touching player ");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
