using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRoomDelayControllor : MonoBehaviour
{
    public MonoBehaviour trapScript;
    public float TriggerTime;
    float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        trapScript.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
    IEnumerator TrapScriptStartDelay()
    {
        yield return new WaitForSeconds(TriggerTime);
        trapScript.enabled = true;
        yield break;
    }



}
