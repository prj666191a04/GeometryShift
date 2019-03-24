using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisspearingPlatform : MonoBehaviour
{

    float health = 1.5f;
    float maxHealth = 1.5f;
    float regenMultiplier = 0.3f;
    public bool isBeingTouched = false;
    int timesCheckedPerSecond = 10;
    float updateInterval;
    Collider[] collidersArray;
    MeshRenderer meshRenderer;
    Renderer theRenderer;
    Collider theActualCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isBeingTouched = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isBeingTouched = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        updateInterval = 1f / timesCheckedPerSecond;
        InvokeRepeating("Update10FPS", updateInterval, updateInterval);
        theRenderer = gameObject.GetComponent<Renderer>();
        collidersArray = GetComponents<Collider>();
        health = maxHealth;

        foreach (Collider c in collidersArray)
        {
            if (!c.isTrigger)
            {
                theActualCollider = c;
            }
        }
    }
    
    void Update10FPS()
    {
        if (isBeingTouched)
        {
            health -= updateInterval;
        }
        else
        {
            health += (updateInterval * regenMultiplier);
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }

        if (health <= 0)
        {
            health = 0;
            theActualCollider.enabled = false;
        }
        else
        {
            theActualCollider.enabled = true;
            theRenderer.material.color = new Color(
                1f, 1f, 1f, health / maxHealth);
        }
    }
}
