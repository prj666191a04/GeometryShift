using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelResults1 : LevelRessultScreenBase
{
    public Text rsltText;
    public Text titleText;
    public Button continueButton;
    public CanvasGroup canvasGroup;


    public override void Play()
    {
        StartCoroutine(PopIn());
    }

    public void OkClick()
    {
        owner_.ConfirmRessults();
    }

    public override void SetRessults(string ressultString)
    {
        rsltText.text = ressultString;
    }
    
    IEnumerator PopIn()
    {
        transform.localScale = Vector3.zero;
        float scale = 0;
        float yScale = 0.1f;
        while (transform.localScale.x != 1)
        {
            yield return new WaitForSeconds(0.01f);
            scale += Time.deltaTime * 6;
            transform.localScale = new Vector3(scale, 0.1f, 1f);
            if (transform.localScale.x > 1)
            {
                transform.localScale = new Vector3(1f, 0.1f, 1f);
            }
        }
        while (transform.localScale != Vector3.one)
        {
            yield return new WaitForSeconds(0.01f);
            yScale += Time.deltaTime * 4;
            transform.localScale = new Vector3(scale, yScale, 1);
            if (transform.localScale.y > 1)
            {
                transform.localScale = Vector3.one;
            }
        }
        yield break;
    }
}
