
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateFloat : MonoBehaviour
{
    Vector3 startPos;
    Vector3 topPos;
    Vector3 targetPos;
    bool top = true;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        topPos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        targetPos = topPos;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Hover();
    }
    void Rotate()
    {
        transform.Rotate(new Vector3(0,0, 1) * (Time.deltaTime * 50));
    }

    void Hover()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime/2 );
        if(Vector3.Distance(transform.position, targetPos) < 0.25)
        {
            top = !top;
            if(top)
            {
                targetPos = topPos;
            }
            else
            {
                targetPos = startPos;
            }
        }
    }

}
