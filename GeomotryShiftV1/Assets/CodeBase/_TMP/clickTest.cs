using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickTest : MonoBehaviour
{
    GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width ,Screen.height, 10));
        
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,10)));
    }
}
