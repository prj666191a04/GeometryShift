//Author Atilla Puskas

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideShuffle : MonoBehaviour
{
    public float interval;
    public float moveSpeed;


    Vector3 pos1;
    Vector3 pos2;
    Vector3 targetPos;
    public int offset = -3;

    bool state = false;
    bool canChange = true;

    // Start is called before the first frame update
    void Start()
    {
        pos1 = transform.localPosition;
        pos2 = new Vector3(pos1.x, pos1.y, pos1.z + offset);
        
        targetPos = pos2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * moveSpeed);
        if(Vector3.Distance(transform.localPosition, targetPos) < 0.1 && canChange)
        {
            canChange = false;
            StartCoroutine(StateChange());
        }
    }

    IEnumerator StateChange()
    {
        yield return new WaitForSeconds(interval);
        state = !state;
        if(state)
        {
            targetPos = pos1;
        }
        else
        {
            targetPos = pos2;
        }
        canChange = true;
        yield break;
    }
}
