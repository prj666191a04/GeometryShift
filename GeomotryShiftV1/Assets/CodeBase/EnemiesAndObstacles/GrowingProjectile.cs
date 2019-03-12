using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingProjectile : EnemyProjectile
{
    public float sizeUp = 2f;
    public float acceleration = 0f;
    float temp;
    // Start is called before the first frame update
    void Start()
    {
        goThroughWalls = true;
    }

    // Update is called once per frame
    void Update()
    {
        shouldDespawn();
        MoveForward();
        temp = sizeUp * Time.deltaTime;
        transform.localScale += new Vector3(temp, temp, temp);
        speed += Time.deltaTime * acceleration;
    }
}
