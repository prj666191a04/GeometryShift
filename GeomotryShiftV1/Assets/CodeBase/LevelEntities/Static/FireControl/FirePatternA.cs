using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePatternA : MonoBehaviour
{
    public GameObject obsticle;
    public int ammount;
    public int speed;
    public int fireAngle;
    public float fireRate;
    float lastFireTime;

    bool fireStandbye = false;
    // Start is called before the first frame update
    void Start()
    {
        lastFireTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - lastFireTime > fireRate)
        {
            Fire();
        }
    }
    private void FixedUpdate()
    {

    }
    
    void Fire()
    {
        for (int i = 0; i < ammount; i++)
        {
            GameObject ob = GameObject.Instantiate(obsticle);
            ob.transform.position = this.transform.position;
            ob.transform.parent = this.transform;
            ob.transform.localRotation = Quaternion.Euler(new Vector3(0, (fireAngle / ammount) * i, 0));
            ob.transform.position = this.transform.position + ob.transform.forward * 1.5f;
            ob.GetComponent<Rigidbody>().velocity = ob.transform.forward * 10;
            fireStandbye = true;
            lastFireTime = Time.time;
        }
    }
}
