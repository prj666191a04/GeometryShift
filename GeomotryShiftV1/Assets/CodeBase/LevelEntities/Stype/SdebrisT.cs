//Author Atilla puskas
//Desc overridden version of Sfebris

//Reason, orginal class behaved strangly when parented to a moving transform
//solution write an additinal fixed update call to move the object instead of velocety
//Orginal version was also kept beacuse it may still be useful in other level types

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
