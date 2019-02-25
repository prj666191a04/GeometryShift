using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelS1 : LevelBase
{

    public RectTransform t1;
    public RectTransform t2;
    public RectTransform t3;
    public RectTransform t4;


    public Vector3 p1;

    GameObject newObject;
    // Start is called before the first frame update
    void Start()
    {
        newObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newObject.transform.SetParent(GetComponent<LevelInit>().parentObject.transform);
        
        p1 =  Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.transform.position.y));

        newObject.transform.position = new Vector3(p1.x, 10, p1.y);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {

            p1 = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,45));
            newObject.transform.position = p1;

        }
       
    }
}
