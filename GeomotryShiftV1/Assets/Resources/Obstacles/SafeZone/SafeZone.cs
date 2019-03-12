using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public int selfDestro;
    //the safe zone is an object which allows the player to pass though, but
    //destroys any object which has the string "Enemy" in its name
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SelfDestruct()
    {
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
