using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    float spinAnglesPerSecond = 360f;
    // Start is called before the first frame update
    void Start()
    {
        Random random = new Random();
        spinAnglesPerSecond = Random.Range(120f, 1200f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, spinAnglesPerSecond * Time.deltaTime, 0f);
    }
}
