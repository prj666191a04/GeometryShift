using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //this is a simple platform that moves back and forth in the X direction
    //this script only handles the platform movement
    //a separate script called AttachPlayerToMovingPlatform does exactly what it says

    bool movingTowardsPositiveX = true;
    float highestXPositionAllowed = 3; //when the platform gets here, it turns around
    float lowestXPositionAllowed = -1; //when the platform gets here, it turns around
    float movementSpeed = 1.5f;//speed in meters per second

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (movingTowardsPositiveX)
        {
            transform.position = new Vector3(transform.position.x + (movementSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        }
        else {
            transform.position = new Vector3(transform.position.x - (movementSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        }


        if (transform.position.x > highestXPositionAllowed)
        {
            movingTowardsPositiveX = false;//turn around
        }
        else if (transform.position.x < lowestXPositionAllowed)
        {
            movingTowardsPositiveX = true;//turn around
        }
    }
}
