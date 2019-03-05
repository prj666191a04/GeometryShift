using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelIntroS1 : OverlayUIAtribute
{



    private RectTransform pos;

    public TMP_Text title;
    public TMP_Text counter;

    public string finishText = "Survive";

    Vector3 targetPos;
    Vector3 startPos;

    private float time;
    private float counterValue = 3f;
    bool destReached;
    bool finished;

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    public override void Play()
    {
        init();
    }

    void init()
    {
        time = 0;
        counterValue = 3f;
        destReached = false;
        finished = false;
        pos = GetComponent<RectTransform>();
        targetPos = pos.anchoredPosition3D;
        startPos = new Vector3(targetPos.x, targetPos.y + 150, targetPos.z);
        pos.anchoredPosition3D = startPos;
    }

    void StartIntro()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
            if (time < 1 && !destReached)
                time += Time.deltaTime;

            if (time > 1)
                time = 1;

            AnimateDown();

            if (destReached)
            {
                if (counterValue > 0)
                {
                    counterValue -= Time.deltaTime;
                    counter.text = Mathf.Round(counterValue).ToString();
                }
                else
                {
                    if (!finished)
                    {
                        counter.text = finishText;
                        finished = true;
                    }
                    else
                    {
                        if(time > 0)
                        {
                            time -= Time.deltaTime;
                        }
                        else
                        {
                            owner_.EndIntro();
                        }
                    }
                }
            }
        
    }
    void AnimateDown()
    {
        pos.anchoredPosition3D = Vector3.Lerp(startPos, targetPos, time);
        if (pos.anchoredPosition3D == targetPos)
            destReached = true;
           
    }
}
