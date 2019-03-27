using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage = 1f;
    public float speed = 6f;
    public float maximumLifespanAllowed = 10f;
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
            if (!other.gameObject.CompareTag("Enemy"))
            {
                print("coll with tag " + other.gameObject.tag + 
                    " and name " + other.gameObject.name);
                Destroy(gameObject);
            }
        }
    }

    protected void InitializeEvents()
    {
        SurvivalLevel1EnemySpawner.despawnEnemies += DestroyThis;
    }

    protected virtual void Awake()
    {
        InitializeEvents();
    }
    // Start is called before the first frame update
    void Start()
    {
        //InitializeEvents();
    }

    protected virtual void OnDestroy()
    {
        SurvivalLevel1EnemySpawner.despawnEnemies -= DestroyThis;
    }

    protected virtual void DestroyThis()
    {
        Destroy(gameObject);
    }

    protected void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    protected void shouldDespawn()
    {
        timeExistedInSeconds += Time.deltaTime;
        if (timeExistedInSeconds > maximumLifespanAllowed)
        {
            Destroy(gameObject);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        shouldDespawn();
        MoveForward();
    }
}
