using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMotor : MonoBehaviour {

    // Use this for initialization
    public float h_ = 0f;
    public float v_ = 0f;
    protected Rigidbody rBody;

    private void FixedUpdate()
    {
        OnFixedUpdate();
    }
    private void Update()
    {
        CollectCustomInput();
    }


    public virtual void SetPhysics(Rigidbody phys)
   {
        rBody = phys;
        ConfigurePhysics();
   }
   
    public virtual void CollectCustomInput()
    {

    }
   
   protected virtual void ConfigurePhysics()
   {


   }

   protected virtual void OnFixedUpdate()
   {

   }

}