using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishPlatformInit : MonoBehaviour
{
    public GameObject vanishPlatform;
    // Start is called before the first frame update
    void Start()
    {
        for (int i2 = -3; i2 < 4; i2++)
        {
            for (int i = -3; i < 4; i++)
            {
                SpawnPlatform((float)(i2 * 2.75), (float)(i * 2.75));
            }
        }
    }

    void SpawnPlatform(float x, float z)
    {
        Vector3 spawnPosition = new Vector3(x, -1f, z);
        Instantiate(vanishPlatform, spawnPosition, new Quaternion(), transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
