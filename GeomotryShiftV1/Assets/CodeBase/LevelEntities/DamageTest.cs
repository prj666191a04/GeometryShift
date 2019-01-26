using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.GetComponent<CStatus>())
        {
            collision.gameObject.GetComponent<CStatus>().Damage(1f);
        }
    }
}
