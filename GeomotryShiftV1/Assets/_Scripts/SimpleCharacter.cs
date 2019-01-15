using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacter : MonoBehaviour
{
    public CharacterController myController;
    // Start is called before the first frame update
    public float speedMultiplier = 6;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 myVector = new Vector3(0, 0, 0);
        myVector.x = Input.GetAxis("Horizontal");
        myVector.z = Input.GetAxis("Vertical");
        myVector.x *= speedMultiplier;
        myVector.z *= speedMultiplier;
        myController.SimpleMove(myVector);
    }
}
