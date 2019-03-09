//Author Atilla puskas
//Description: Collects horizontal and verticle input from the game


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this class is experminental and might be replaced entirly.
public class GSInput : MonoBehaviour {

    private static float verticle = 0f;
    private static float horizontal = 0f;

    private KeyCode NorthKey = KeyCode.W;
    private KeyCode SouthKey = KeyCode.S;
    private KeyCode EastKey = KeyCode.D;
    private KeyCode WestKey = KeyCode.A;


   


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SetAxis(NorthKey, SouthKey, ref verticle);
        SetAxis(EastKey, WestKey, ref horizontal);

	}

    public static float GetHAxis()
    {
        return horizontal;
    }
    public static float GetVAxis()
    {
        return  verticle;
    }

    private void SetAxis(KeyCode positive, KeyCode negative, ref float value)
    {
        value = ((Input.GetKey(positive)) ? 1 : 0)
              + ((Input.GetKey(negative)) ? -1 : 0);
    }

}
