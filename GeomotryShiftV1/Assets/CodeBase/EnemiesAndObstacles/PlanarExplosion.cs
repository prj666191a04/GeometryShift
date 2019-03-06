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
    

    public bool usingMat1 = true;

    public GameObject theEnemyToSpawn;

    MeshRenderer meshRenderer;
    Renderer theRenderer;

    Color color1;
    Color color2;

    // Start is called before the first frame update
    void Start()
    {
        if (maximumLifespanAllowed <= 0)
        {
            Explode();
        }
        theRenderer = gameObject.GetComponent<Renderer>();
        meshRenderer = GetComponent<MeshRenderer>();
        if (theEnemyToSpawn == null)
        {
            print("error: planar explosion doesn't know what enemy to spawn");
        }
        color1 = new Color(1f, 0, 0, 0.7f);
        color2 = new Color(1f, 0.5f, 0, 0.7f);
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
        timeExistedInSeconds += Time.deltaTime;
        secondsSinceLastMatChange += Time.deltaTime;
        if (secondsSinceLastMatChange > secondsBetweenMatChange)
        {
            secondsSinceLastMatChange -= secondsBetweenMatChange;
            if (usingMat1)
            {
                usingMat1 = false;
                theRenderer.material.color = color1;
            }
            else
            {
                usingMat1 = true;
                theRenderer.material.color = color2;
            }
        }
        if (timeExistedInSeconds > maximumLifespanAllowed)
        {
            Explode();
        }
    }
}
