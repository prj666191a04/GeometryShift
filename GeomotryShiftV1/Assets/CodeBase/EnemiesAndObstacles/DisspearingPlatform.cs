﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisspearingPlatform : MonoBehaviour
{
    public Material defaultMaterial;
    public Material aboutToDissapearMaterial;
    public Material hasDissapearedMaterial;

    float secondsSinceTouchedByPlayer;
    bool dissapearing = false;
    Collider[] collidersArray;
    MeshRenderer meshRenderer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dissapearing = true;
            meshRenderer.material = aboutToDissapearMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //do something
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material = defaultMaterial;
        collidersArray = GetComponents<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dissapearing)
        {
            secondsSinceTouchedByPlayer += Time.deltaTime;
        }
        if (secondsSinceTouchedByPlayer > 1)
        {
            foreach (Collider c in collidersArray)
            {
                c.enabled = false;
            }
            meshRenderer.material = hasDissapearedMaterial;

        }
        if (secondsSinceTouchedByPlayer > 2.5)
        {
            foreach (Collider c in collidersArray)
            {
                c.enabled = true;
            }
            meshRenderer.material = defaultMaterial;
            dissapearing = false;
            secondsSinceTouchedByPlayer = 0f;
        }
    }
}
