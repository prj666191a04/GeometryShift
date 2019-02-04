using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatingPlatform : MonoBehaviour
{
    public Material state1Material;
    public Material state2Material;
    
    Collider[] collidersArray;
    MeshRenderer meshRenderer;
    float secondsPassed;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = state1Material;
        collidersArray = GetComponents<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        secondsPassed += Time.deltaTime;  
        if (secondsPassed > 2)
        {
            secondsPassed = 0;
        }
        if (secondsPassed > 1)
        {
            foreach (Collider c in collidersArray)
            {
                c.enabled = false;
            }
            meshRenderer.material = state2Material;

        }
        else
        {
            foreach (Collider c in collidersArray)
            {
                c.enabled = true;
            }
            meshRenderer.material = state1Material;
        }
    }
}
