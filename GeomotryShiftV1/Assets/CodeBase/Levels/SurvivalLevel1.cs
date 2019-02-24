using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalLevel1 : MonoBehaviour
{
    float secondsPassed = 0;
    public float secondsNeededToSurviveToWin = 3;
    Random random = new Random();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void whatEnemiesShouldSpawn()
    {

    }

    // Update is called once per frame
    void Update()
    {
        secondsPassed += Time.deltaTime;
        print(secondsPassed + "rng 1-3 " + Random.Range(1,3));
        if (secondsPassed > secondsNeededToSurviveToWin)
        {
            LevelBase.instance.AcknowledgeLevelCompletion();
        }
    }
}
