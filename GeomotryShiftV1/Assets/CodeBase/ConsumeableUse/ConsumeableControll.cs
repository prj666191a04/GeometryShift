using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeableControll : MonoBehaviour
{
    public bool active = true;

    public ConsumeableActiveSlot recoverySlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      InputResponse();     
    }

    void InputResponse()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Alpha 1");
            recoverySlot.Activate();
        }
    }



}
