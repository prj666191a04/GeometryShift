using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMotor : MonoBehaviour {

    // Use this for initialization
    public float h_ = 0f;
    public float v_ = 0f;
    protected Rigidbody rBody;
    protected CController controller_;
    

    private void FixedUpdate()
    {
        
    }
    private void Update()
    {
        
    }


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