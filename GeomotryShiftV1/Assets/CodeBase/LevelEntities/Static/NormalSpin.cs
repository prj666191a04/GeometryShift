//Author Atilla Puskas

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSpin : MonoBehaviour
{
    public float Speed = 30f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, Speed * Time.deltaTime, 0));
    }
}
