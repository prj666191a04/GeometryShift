using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage = 1.5f;
    public float speed = 2f;
    public float maximumLifespanAllowed = 4f;
    public float timeExistedInSeconds = 0f;
    public bool goThroughWalls = true;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("player was hit by enemy projectile OnTriggerEnter");
            other.gameObject.GetComponent<CStatus>().Damage(damage);
            Destroy(gameObject);
        }
        print("on collision enter name " + other.gameObject.name);
        if (!goThroughWalls)
        {
            if (!other.gameObject.name.Contains("Enemy"))
            {
                Destroy(gameObject);
            }
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeExistedInSeconds += Time.deltaTime;
        if (timeExistedInSeconds > maximumLifespanAllowed)
        {
            Destroy(gameObject);
        }
        //transform.position += transform.forward * Time.deltaTime * speed;
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }
}
