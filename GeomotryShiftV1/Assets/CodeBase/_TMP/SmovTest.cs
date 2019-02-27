using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmovTest : MonoBehaviour
{
    float h_;
    float v_;
   public Vector2 screenBounds;
    public Vector3 moveDirection;
    public Vector2 characterExtants;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        characterExtants = Camera.main.ScreenToWorldPoint(new Vector3 (0.5f, 0.5f, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        h_ = GSInput.GetHAxis();
        v_ = GSInput.GetVAxis();
        moveDirection = new Vector3(h_, v_, 0);
        if (moveDirection != Vector3.zero)
        {
            transform.Translate((moveDirection * Time.deltaTime) * 5);
        }

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.scaledPixelWidth, Camera.main.scaledPixelHeight, Camera.main.transform.position.z));
        Vector3 cpos = transform.position;
        cpos.x = Mathf.Clamp(cpos.x, (screenBounds.x + 0.5f), (screenBounds.x) * -1 - 0.5f);
        cpos.y = Mathf.Clamp(cpos.y, (screenBounds.y + 0.5f), (screenBounds.y) * -1 - 0.5f) ;
        transform.position = cpos;
    }
}
