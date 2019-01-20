using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInteraction : MonoBehaviour
{
    public LayerMask mask;
    public float radius = 1f;
    public float maxDistance = 1f;
   
    private Vector3 pos;
    public Vector3 direction;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        distance = maxDistance;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        pos = transform.position;
        direction = Vector3.zero;

        
        if (Physics.SphereCast(pos, radius , direction ,out hit, mask))
        {
            distance = hit.distance;
            if(distance <= 1 )
             {
                 Debug.Log("in range");
             }

        }
        

    }


    void Scan()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(pos + direction * distance, radius);
    }
}
