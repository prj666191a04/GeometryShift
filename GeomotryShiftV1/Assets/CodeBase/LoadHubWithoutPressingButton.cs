using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadHubWithoutPressingButton : MonoBehaviour
{
    public GameObject theEnvironmentContainer;
    public GameObject theLevelToBeLoadedImmediately;
    bool hasDone = false;
    // Start is called before the first frame update
    void Start()
    {
        //LevelLoader a = new LevelLoader();
        //a.EnvironmentContainer = theEnvironmentContainer.transform;
        //.LoadLevel(theLevelToBeLoadedImmediately);

        LevelLoader.instance.LoadLevel(theLevelToBeLoadedImmediately);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDone)
        {
            LevelLoader.instance.LoadLevel(theLevelToBeLoadedImmediately);
            hasDone = true;
            Destroy(gameObject);
        }
    }
}
