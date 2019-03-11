using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author Atilla puskas
//Description: a base class to be used for all movement scripts in the game


public class CMotor : MonoBehaviour {

    // Use this for initialization
    public float h_ = 0f;
    public float v_ = 0f;
    protected Rigidbody rBody;
    public CController controller_;
    

    public virtual void SetPhysics(Rigidbody phys)
    {
        rBody = phys;
        ConfigurePhysics();
    }
   
    public virtual void CustomInput()
    {

    }
   
   protected virtual void ConfigurePhysics()
   {


   }

   protected virtual void OnFixedUpdate()
   {

   }

}