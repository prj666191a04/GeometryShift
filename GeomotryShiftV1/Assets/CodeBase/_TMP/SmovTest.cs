using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmovTest : MonoBehaviour
{
    float h_;
    float v_;
    Vector2 screenBounds;
    public Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
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
        Vector3 cpos = transform.position;
        cpos.x = Mathf.Clamp(cpos.x, screenBounds.x, screenBounds.x * -1);
        cpos.y = Mathf.Clamp(cpos.y, screenBounds.y, screenBounds.x * -1);
        transform.position = cpos;
    }
}
