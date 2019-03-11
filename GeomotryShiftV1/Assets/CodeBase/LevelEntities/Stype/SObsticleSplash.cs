//Author Atilla puskas
//Description: a composite obsticle for the player to avoid

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//obsticle must have a ridgid body!
public class SObsticleSplash : MonoBehaviour
{
    public GameObject obsticle;
    public int ammount;
    public int speed;
    public int lifeTime = 3;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(time > lifeTime)
        {
           Destroy(this.gameObject);
        }
    }
    private void FixedUpdate()
    {
        //transform.Rotate(Vector3.up * 100 * Time.deltaTime);
    }

    void Spawn()
    {
        for (int i = 0; i < ammount; i++)
        {
            GameObject ob = GameObject.Instantiate(obsticle);
            ob.transform.position = this.transform.position;
            ob.transform.parent = this.transform;      
            ob.transform.localRotation = Quaternion.Euler(new Vector3(0, (360 / ammount) * i, 0));
            ob.transform.position = this.transform.position + ob.transform.forward * 1.5f;
            //ob.GetComponent<Rigidbody>().velocity = ob.transform.forward * 10;
            
        }
    }
    
}
