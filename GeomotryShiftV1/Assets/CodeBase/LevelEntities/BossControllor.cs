//Atilla Puskas
//Desc Controlls the bossfight in the final level

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossControllor : MonoBehaviour
{

    public GameObject misslePrefab;
    public GameObject bulletPrefab;

    public Transform bossTransform;
    public Transform playerTransform;


    public Transform[] missleLaunchPoints;
    Rigidbody rBody;

    public List<BossMovementPattern> movementPatterns;
    int pattenrIndex = 0;
    int navIndex = 0;
    float speed = 3f;
    int phase = 0;
    

    Vector3 targetPosition;
    Vector3 targetDirection;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(movementPatterns[pattenrIndex].navPoints[navIndex]);
    }
    private void FixedUpdate()
    {
       
    }


    void moveToPoint()
    {
        Vector3 target = movementPatterns[pattenrIndex].navPoints[navIndex].position;

        Vector3 heading = target - transform.position;
        heading.y = 0;

        if (Vector3.Distance(transform.position, movementPatterns[pattenrIndex].navPoints[navIndex].position) > 0.1)
        {
            rBody.MovePosition(transform.position += heading * speed * Time.deltaTime);
        }
        else
        {
            if (navIndex < movementPatterns[pattenrIndex].navPoints.Count - 1)
            {
                navIndex++;
                Debug.Log("index Increase");
            }
            else
            {
                navIndex = 0;
            }
        }
        
    }

    void SetRotation()
    {


        Vector3 target = movementPatterns[pattenrIndex].navPoints[navIndex].position;

        Vector3 heading = target - transform.position;

        if (targetDirection != Vector3.zero)
        {
            //Set the target Direction
            //targetRotation = Quaternion.LookRotation(targetDirection, transform.up);

        }
    }
   //Preform this frames rotation
          //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime* 10);



}


[System.Serializable]
public class BossMovementPattern {
    public List<Transform> navPoints;
    
}

