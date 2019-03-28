//Author Atilla Puskas
//Controls the UI elements on Timed levels

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class TimerCanvas : MonoBehaviour
{
    public Text timer;

    public Color normalColor;
    public Color warningColor;
    public Color dangerColor;
    public Canvas canvas;
    
     Color targetColor;


    private void OnEnable()
    {
        CStatus.OnPlayerDeath += DeathReset;
    }
    private void OnDisable()
    {
        CStatus.OnPlayerDeath -= DeathReset;
    }

    void DeathReset(int m = 0)
    {
        StopAllCoroutines();
        SetNormalColor();
    }

    // Start is called before the first frame update
    void Awake()
    {
        targetColor = normalColor;

    }

    // Update is called once per frame
    void Update()
    {
        timer.color = Color.Lerp(timer.color, targetColor, Time.deltaTime * 3f);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetText(float time_)
    {
        System.TimeSpan time = System.TimeSpan.FromSeconds(time_);
        timer.text = time.ToString(@"mm\:ss\:ff");
    }

    public void SetWarningColor()
    {
        StartCoroutine(SetWarningColorEnum());
    }

    private IEnumerator SetWarningColorEnum()
    {
        Debug.Log("warning Color Set");
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            timer.color = warningColor;
        }
        targetColor = warningColor;
      
    }

    public void SetNormalColor()
    {
        targetColor = normalColor;
        timer.color = normalColor;
    }
    public void SetDangerColor()
    {
        targetColor = dangerColor;
        timer.color = dangerColor;
    }

}
