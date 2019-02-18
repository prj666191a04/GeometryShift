using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage = 1.5f;
    public float speed = 2f;
    public float maximumLifespanAllowed = 2f;
    public float timeExistedInSeconds = 0f;
    public bool goThroughWalls = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CStatus>().Damage(damage);
            Destroy(gameObject);
        }

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
