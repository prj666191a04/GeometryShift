using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalFood : MonoBehaviour
{
    public float widthOfLevel = 0f;
    public float lengthOfLevel = 0f;
    public GameObject textContainer;
    static Hunger playerHungerScript;
    TMPro.TextMeshPro theText;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SpawnNewFood(transform.parent);
            playerHungerScript.timeSinceLastAte = 0f;
            Destroy(gameObject);
        }
        
    }

    public void SpawnNewFood(Transform theParent)
    {
        //when food is eaten, it is replaced by a new one
        Vector3 spawnPosition = new Vector3(Random.Range(-(SurvivalLevel1EnemySpawner.widthOfLevel / 2.5f),
            (SurvivalLevel1EnemySpawner.widthOfLevel / 2.5f)), 0f,
            Random.Range(-(SurvivalLevel1EnemySpawner.lengthOfLevel / 2.5f),
            (SurvivalLevel1EnemySpawner.lengthOfLevel / 2.5f)));

        Instantiate(this, spawnPosition, new Quaternion(), theParent);

    }
    // Start is called before the first frame update
    void Start()
    {
        if (playerHungerScript == null)
        {
            playerHungerScript = GeometryShift.playerStatus.gameObject.GetComponent<Hunger>();
        }
        theText = textContainer.GetComponent<TMPro.TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        float num = playerHungerScript.timeSinceLastAteLimit - playerHungerScript.timeSinceLastAte;
        num = (float)System.Math.Round(num, 2);
        theText.text = num.ToString();
        if (num > playerHungerScript.timeSinceLastAteLimit * 0.6)
        {
            theText.color = Color.cyan;
        }
        else if (num > playerHungerScript.timeSinceLastAteLimit * 0.3)
        {
            theText.color = Color.yellow;
        }
        else
        {
            theText.color = Color.red;
        }
    }
}
