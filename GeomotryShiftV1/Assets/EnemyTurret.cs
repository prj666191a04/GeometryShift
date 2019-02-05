using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    float secondsPassed = 0f;
    float attackInterval = 1.5f;
    public GameObject thePrefab;
    public GameObject theTarget;
    public bool trackTarget = true;

    void Shoot()
    {
        thePrefab.GetComponent<EnemyProjectile>().speed = 12;
        if (trackTarget)
        {
            transform.LookAt(theTarget.transform);
        }
        Instantiate(thePrefab, transform.position, transform.rotation, transform.parent);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        secondsPassed += Time.deltaTime;
        if (secondsPassed > attackInterval)
        {
            secondsPassed = 0;
            Shoot();
        }
    }
}
