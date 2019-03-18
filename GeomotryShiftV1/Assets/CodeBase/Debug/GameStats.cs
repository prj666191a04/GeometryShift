using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour
{
    public Text fpsCounter;
    float frameRate;
    // Start is called before the first frame update
    void Start()
    {
      // Debug.Log(QualitySettings.vSyncCount);
      // QualitySettings.vSyncCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
       //  frameRate = 1.0f / Time.deltaTime;
       //  fpsCounter.text = frameRate.ToString();
    }
}
