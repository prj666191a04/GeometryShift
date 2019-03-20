using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeableControll : MonoBehaviour
{
    bool active = false;

    ConsumeableActiveSlot recoverySlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            InputResponse();
        }
    }

    void InputResponse()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            recoverySlot.Activate();
        }
    }



}
