using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMotor : MonoBehaviour {

    // Use this for initialization
    public float h_ = 0f;
    public float v_ = 0f;
    protected Rigidbody rBody;

   public virtual void SetPhysics(Rigidbody phys)
   {
        rBody = phys;
        ConfigurePhysics();
   }
   
   protected virtual void ConfigurePhysics()
   {


   }

}