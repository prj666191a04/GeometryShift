using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInteraction : MonoBehaviour
{

    GameObject target;

    //Many of these values are redundant and for testing purposes only
    public Collider[] obj;
    public CInteractable targetObj;

   
    public LayerMask mask;
    public float radius = 1f;
    public float maxDistance = 1f;
   
    private Vector3 pos;
    public Vector3 direction;
    public float distance;
    bool responseGiven = false;

    private bool uiSet = false;


    // Start is called before the first frame update
    void Start()
    {
        distance = maxDistance;
        //obj = new Collider[3];
    }

    // Update is called once per frame
    void Update()
    {
        Scan();
       
    }

    void Scan()
    {
     //TODO: This code is messy and must be cleaned up and factored
        //RaycastHit hit;
        pos = transform.position;
        //direction = Vector3.zero;

        //if (Physics.SphereCast(pos, radius, direction, out hit, maxDistance, mask))
        //{
        //    Debug.Log("hit");
        //    distance = hit.distance;
        //    if (distance <= 2)
        //    {
        //        Debug.Log("in range");
        //    }

        //}

        //TODO: Remove hard coded key and look into using the NonAlloc version of the function instead, if time allows.
        //Physics.OverlapSphereNonAlloc
        obj = Physics.OverlapSphere(pos, radius, mask);
        if (obj.Length > 0)
        {
            if(targetObj != obj[0] && !responseGiven)
            {
                targetObj = obj[0].gameObject.GetComponent<CInteractable>();
                targetObj.Respond();
                responseGiven = true;
                Debug.Log("resonseCall");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                targetObj.Trigger();
                UnsetUI();
                responseGiven = false;
            }       
        }
        else if (targetObj != null)
        {
            targetObj = null;
            UnsetUI();
            responseGiven = false;
        }
    }

    void UnsetUI()
    {
        GeometryShift.instance.interactionUI.Hide();
    }
    //For debug purposes only - remove for finished product
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(pos + direction * distance, radius);
    }
}
