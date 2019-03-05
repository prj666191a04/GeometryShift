using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalLevel1 : MonoBehaviour
{
    float secondsPassed = 0;
    float secondsNeededToSurviveToWin = 80;
    Random random = new Random();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*secondsPassed += Time.deltaTime;
        if (secondsPassed > secondsNeededToSurviveToWin)
        {
            LevelBase.instance.AcknowledgeLevelCompletion();
        }*/
    }
}
