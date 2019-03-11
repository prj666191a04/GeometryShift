//Author Atilla
//Class to move flow object around for S type levels
//MVP implementation only good enough to test other code may need to be discontinued due to time contraints

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmapFlow : MonoBehaviour
{
    Vector3 targetPosition;
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * 10 * Time.deltaTime;
    }
}
