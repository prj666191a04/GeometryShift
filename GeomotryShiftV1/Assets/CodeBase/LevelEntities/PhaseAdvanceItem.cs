using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseAdvanceItem : MonoBehaviour
{
    public delegate void ShouldAdvance();
    public static event ShouldAdvance gotCollected;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SurvivalManualAdvance.keysRemaining--;
            gotCollected?.Invoke();
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
