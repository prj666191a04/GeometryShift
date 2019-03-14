using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    public float timeSinceLastAte = 0f;
    public float timeSinceLastAteLimit = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAte += Time.deltaTime;
        if (timeSinceLastAte > timeSinceLastAteLimit)
        {
            gameObject.GetComponent<CStatus>().Damage(9999);
        }
    }
}
