using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelS1 : LevelBase
{


    public float progress = 0;

    public GameObject pointTest;

    public RectTransform t1;
    public RectTransform t2;
    public RectTransform t3;
    public RectTransform t4;

    LevelInit init;
    CameraControllerA cameraController;
    public Vector3 screenBounds;

    Vector3 spawnPointBL;
    Vector3 spawnPointBR;
    Vector3 spawnPointCL;
    Vector3 spawnPointCR;
    Vector3 spawnPointTL;
    Vector3 spawnPointTR;

    // Start is called before the first frame update
    void Start()
    {
        init = GetComponent<LevelInit>();
        Debug.Log("levelS1");
        cameraController = Camera.main.GetComponent<CameraControllerA>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.scaledPixelWidth, Camera.main.scaledPixelHeight, cameraController.offset.z));
        StartCoroutine(LateStart()); 
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.2f);
        SetupLevelSpawnPoints();
        yield break;
    }

    void SetupLevelSpawnPoints()
    {
        spawnPointBL = new Vector3(screenBounds.x/2, screenBounds.y/2, 0);
        spawnPointBR = new Vector3((screenBounds.x * -1) / 2, screenBounds.y / 2, 0);
        spawnPointCL = new Vector3(screenBounds.x/2, 0, 0);
        spawnPointCR = new Vector3((screenBounds.x * -1) / 2, 0 / 2, 0);
        spawnPointTL = new Vector3(screenBounds.x / 2, (screenBounds.y * -1) / 2, 0);
        spawnPointTR = new Vector3((screenBounds.x * -1) / 2, (screenBounds.y * -1) / 2, 0);

        GameObject t1 = Instantiate(pointTest, spawnPointBL, Quaternion.identity, init.parentObject.transform);
        GameObject t2 = Instantiate(pointTest, spawnPointBR, Quaternion.identity, init.parentObject.transform);
        GameObject t3 = Instantiate(pointTest, spawnPointCL, Quaternion.identity, init.parentObject.transform);
        GameObject t4 = Instantiate(pointTest, spawnPointCR, Quaternion.identity, init.parentObject.transform);
        GameObject t5 = Instantiate(pointTest, spawnPointTL, Quaternion.identity, init.parentObject.transform);
        GameObject t6 = Instantiate(pointTest, spawnPointTR, Quaternion.identity, init.parentObject.transform);
    }


    // Update is called once per frame
    void Update()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.scaledPixelWidth, Camera.main.scaledPixelHeight, cameraController.offset.z));

    }
}
