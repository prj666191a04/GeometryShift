using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousAlternatingPlatform : MonoBehaviour
{
    public Material state1Material;
    public Material state2Material;

    bool state1 = true;
    Collider[] collidersArray;
    MeshRenderer meshRenderer;
    float secondsPassed;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (state1 == false)
            {
                collision.gameObject.GetComponent<CStatus>().Damage(1f);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        state1Material = Resources.Load("Obstacles/DangerAlternatingPlat/DangerAltBlock1") as Material;
        state2Material = Resources.Load("Obstacles/DangerAlternatingPlat/DangerAltBlock2") as Material;


        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = state1Material;
        collidersArray = GetComponents<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        secondsPassed += Time.deltaTime;
        if (secondsPassed > 4)
        {
            secondsPassed = 0;
        }
        if (secondsPassed > 2)
        {
            state1 = false;
            meshRenderer.material = state2Material;
        }
        else
        {
            meshRenderer.material = state1Material;
            state1 = true;
        }
    }
}
