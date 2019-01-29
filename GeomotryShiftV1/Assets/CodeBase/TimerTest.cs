using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTest : MonoBehaviour
{
    int framesPassed = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        print(DspTime(framesPassed / 60));
        framesPassed++;
    }
    static String DspTime(float time)
    {
        String theString;
        int minutes = 0;
        int seconds = 0;
        int milliseconds = 0;

        if (time >= 60)
        {
            minutes = (int)(time / 60);
            time = time % 60;
        }
        if (time >= 1)
        {
            seconds = (int)time;
            time = time % 1;
        }
        if (time > 0)
        {
            milliseconds = (int)((time * 1000) + 0.5);//+0.5 is rounding fix
        }
        theString = minutes.ToString("D2")
            + ":"
            + seconds.ToString("D2")
             + ":"
             + milliseconds.ToString("D2");

        return theString;
    }
}
