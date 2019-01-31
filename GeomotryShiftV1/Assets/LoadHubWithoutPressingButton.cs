using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadHubWithoutPressingButton : MonoBehaviour
{
    public GameObject theEnvironmentContainer;
    public GameObject theLevelToBeLoadedImmediately;
    // Start is called before the first frame update
    void Start()
    {
        LevelLoader a = new LevelLoader();
        a.EnvironmentContainer = theEnvironmentContainer.transform;
        a.LoadLevel(theLevelToBeLoadedImmediately);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
