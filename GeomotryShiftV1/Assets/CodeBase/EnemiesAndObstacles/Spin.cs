using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    float spinAnglesPerSecond =720f;
    // Start is called before the first frame update
    void Start()
    {
        Random random = new Random();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0, spinAnglesPerSecond * Time.deltaTime);
    }
}
