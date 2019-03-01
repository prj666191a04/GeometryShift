using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sdebris : MonoBehaviour
{
    MeshRenderer rend;
    float timeAlive = 0f;
    int maxTime = 10;
    bool deathTriggered = false;
    float transparancy;



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !deathTriggered)
        {
            other.GetComponent<CStatus>().Damage(1f);
            StartCoroutine(FadeAway());
            Debug.Log("Player hit");
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        transparancy = rend.material.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        Tick();
    }


    void Tick()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > maxTime && !deathTriggered)
        {
            StartCoroutine(FadeAway());
        }
    }

    IEnumerator FadeAway()
    {
        deathTriggered = true;
        while(transparancy > 0)
        {
            transparancy -= 0.05f;
            rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, transparancy);
            

            yield return new WaitForSeconds(0.01f);
        }
        Destroy(this.gameObject);
        yield break;
    }


}
