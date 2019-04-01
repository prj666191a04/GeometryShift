using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseAdvanceItem : MonoBehaviour
{
    public delegate void ShouldAdvance();
    public static event ShouldAdvance gotCollected;
    public GameObject particlePrefab;
    

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject tempParticlePrefab = Instantiate(particlePrefab, transform.position, new Quaternion(), transform.parent);
            tempParticlePrefab.gameObject.GetComponent<ParticleSystem>().Emit(100);

            gotCollected?.Invoke();
            Destroy(tempParticlePrefab, 3);//clean up the empty gameobject
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        SurvivalManualAdvance.despawn += DestroyThis;
    }

    private void OnDestroy()
    {
        SurvivalManualAdvance.despawn -= DestroyThis;
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //ps.Emit(60);
    }
}
