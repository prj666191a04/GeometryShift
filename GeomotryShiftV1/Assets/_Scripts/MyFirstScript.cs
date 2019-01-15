using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFirstScript : MonoBehaviour
{
    // Start is called before the first frame update
    public string printText = "default text";
    public int counter = 0;
    void Start()
    {
        print(printText);
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        if (counter % 10 == 0)
        {
            print(counter);
        }
    }
}
