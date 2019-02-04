using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    float secondsPassed = 0f;
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
        Instantiate(thePrefab, transform.position, transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        secondsPassed += Time.deltaTime;
        if (secondsPassed > 1)
        {
            secondsPassed = 0;
            Shoot();
        }
    }
}
