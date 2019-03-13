using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelRetry1 : OverlayUIAtribute
{
    public Button yesBtn;
    public Button noBtn;

    public Text mainText;
    public Text subText;

    


    public override void Play()
    {
        
    }

  

    IEnumerator PopIn()
    {
        transform.localScale = Vector3.zero;
        float scale = 0;
        while (transform.localScale != Vector3.one)
        {
            yield return new WaitForSeconds(0.01f);
            scale += Time.deltaTime * 2;
            transform.localScale = new Vector3(scale, scale, scale);
            if(transform.localScale.x > 1)
            {
                transform.localScale = Vector3.one;
            }
        }

        yield break;
    }
    IEnumerator PopOut()
    {
        transform.localScale = Vector3.one;
        float scale = 1;
        while (transform.localScale != Vector3.one)
        {
            yield return new WaitForSeconds(0.01f);
            scale -= Time.deltaTime * 2;
            transform.localScale = new Vector3(scale, scale, scale);
            if (transform.localScale.x < 0)
            {
                transform.localScale = Vector3.zero;
            }
        }

        yield break;
    }
    private void AnimateOntoScreen()
    {
        StartCoroutine(PopIn());
    }

    private void AnimateOffScreen()
    {
        StartCoroutine(PopOut());
    }
    public void NoBtn()
    {

    }
    public void YesBtn()
    {

    }
}
