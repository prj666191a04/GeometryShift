using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanarExplosion : MonoBehaviour
{
    public float maximumLifespanAllowed = 1f;
    public float timeExistedInSeconds = 0f;
    //public float angle = 0f;
    public int numberOfEnemiesSpawned = 6;

    float secondsSinceLastMatChange = 0f;
    float secondsBetweenMatChange = 0.1f;

    public Material material1;
    public Material material2;

    public bool usingMat1 = true;

    public GameObject theEnemyToSpawn;

    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (theEnemyToSpawn == null)
        {
            theEnemyToSpawn = Resources.Load("Enemies/EnemyTurretFolder/Enemy Projectile") as GameObject;
        }   
        if (maximumLifespanAllowed <= 0)
        {
            Explode();
        }
        if (material1 == null)
        {
            material1 = Resources.Load("Materials/TransparentOrange") as Material;
        }
        if (material2 == null)
        {
            material2 = Resources.Load("Materials/TransparentRed") as Material;
        }
    }

    void Explode()
    {
        float angleInterval = 360f / numberOfEnemiesSpawned;
        float currentAngle = transform.rotation.eulerAngles.y;

        Quaternion spawnRotation;

        for (int i = 0; i < numberOfEnemiesSpawned; i++)
        {
            spawnRotation = Quaternion.Euler(0f, currentAngle, 0f);
            currentAngle += angleInterval;
            Instantiate(theEnemyToSpawn, this.transform.position, spawnRotation, transform.parent);
        }

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //print("dolar " + );
        timeExistedInSeconds += Time.deltaTime;
        secondsSinceLastMatChange += Time.deltaTime;
        if (secondsSinceLastMatChange > secondsBetweenMatChange)
        {
            secondsSinceLastMatChange -= secondsBetweenMatChange;
            if (usingMat1)
            {
                usingMat1 = false;
                meshRenderer.material = material2;
            }
            else
            {
                usingMat1 = true;
                meshRenderer.material = material1;
            }
        }
        if (timeExistedInSeconds > maximumLifespanAllowed)
        {
            Explode();
        }
    }
}
