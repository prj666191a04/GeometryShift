using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisspearingPlatform : MonoBehaviour
{

    float health = 1.8f;
    float maxHealth = 1.8f;

    float timeBeforeRespawn = 1f;
    float timeSinceDissapeared = 0f;

    int timesCheckedPerSecond = 30;
    float updateInterval;

    Collider[] collidersArray;
    Renderer theRenderer;
    Collider theActualCollider;


    public GameObject particlePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InvokeRepeating("Fade", updateInterval, updateInterval);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SurvivalVanishSpawner.regen += Regen;

        updateInterval = 1f / timesCheckedPerSecond;
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


    private void OnDestroy()
    {
        SurvivalVanishSpawner.regen -= Regen;
    }
    

    void Regen()
    {
        CancelInvoke();
        health = maxHealth;
        theRenderer.material.color = new Color(
                0.5f, 0.93f, 1f, 1f);
        GameObject tempParticlePrefab = Instantiate(particlePrefab, transform.position, new Quaternion(), transform.parent);

        tempParticlePrefab.gameObject.GetComponent<ParticleSystem>().Emit(30);

        Destroy(tempParticlePrefab, 3);//clean up the empty gameobject
    }

    void WaitToRespawn()
    {
        timeSinceDissapeared += updateInterval;
        if (timeSinceDissapeared >= timeBeforeRespawn)
        {
            Regen();
        }
    }

    void Fade()
    {
        health -= updateInterval;

        if (health <= 0)
        {
            timeSinceDissapeared = 0;
            health = 0;
            theActualCollider.enabled = false;
            CancelInvoke();
            InvokeRepeating("WaitToRespawn", updateInterval, updateInterval);
        }
        else
        {
            theActualCollider.enabled = true;
            theRenderer.material.color = new Color(
                1f, 1f, 1f, health / maxHealth);
        }
    }
}
