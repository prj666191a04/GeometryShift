using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Copy of SlideShuffle but for different direction
public class SLidingBlocks : MonoBehaviour
{
    public float interval;
    public float moveSpeed;


    Vector3 pos1;
    Vector3 pos2;
    Vector3 targetPos;
    public int offsetX = 0; // ofset in meters
    public int offsetY = 0;
    public int offsetZ = 0;

    bool state = false;
    bool canChange = true;

    // Start is called before the first frame update
    void Start()
    {
        pos1 = transform.localPosition;
        pos2 = new Vector3(pos1.x + offsetX, pos1.y + offsetY, pos1.z + offsetZ);

        targetPos = pos2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * moveSpeed);
        if (Vector3.Distance(transform.localPosition, targetPos) < 0.1 && canChange)
        {
            canChange = false;
            StartCoroutine(StateChange());
        }
    }

    IEnumerator StateChange()
    {
        yield return new WaitForSeconds(interval);
        state = !state;
        if (state)
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
