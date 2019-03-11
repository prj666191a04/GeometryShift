using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SdebrisT : Sdebris
{
    protected override void Tick()
    {
        base.Tick();
        
    }
    private void FixedUpdate()
    {
        rBody.MovePosition(transform.position + transform.forward * 5 * Time.deltaTime);
    }
}
