using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalBillboard : MonoBehaviour
{
    Transform camTrans;
    // Start is called before the first frame update
    void Start()
    {
        camTrans = Camera.main.transform;

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camTrans.rotation;
    }
}
