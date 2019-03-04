using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelS1 : LevelBase
{

    public GameObject obsticleType1;

    public float progress = 0;

    public GameObject pointTest;


    private Coroutine tmpWave;

    LevelInit init;
    CameraControllerA cameraController;
    public Vector3 screenBounds;

    Vector3 spawnPointBL;
    Vector3 spawnPointBR;
    Vector3 spawnPointCL;
    Vector3 spawnPointCR;
    Vector3 spawnPointTL;
    Vector3 spawnPointTR;

    private void OnEnable()
    {
        CStatus.OnPlayerDeath += ResetLevel;
    }

    private void OnDisable()
    {
        CStatus.OnPlayerDeath -= ResetLevel;
    }

    private void ResetLevel(int method = 0)
    {
        StopCoroutine(tmpWave);
        StartCoroutine(DelayedRestart());
    }


    // Start is called before the first frame update
    void Start()
    {
        init = GetComponent<LevelInit>();
        Debug.Log("levelS1");
        cameraController = Camera.main.GetComponent<CameraControllerA>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.scaledPixelWidth, Camera.main.scaledPixelHeight, cameraController.offset.z));
        StartCoroutine(LateStart()); 
    }

    IEnumerator DelayedRestart()
    {
        yield return new WaitForSeconds(10);
        GeometryShift.playerStatus.gameObject.GetComponent<CController>().Resawn(init.spawnPoint.position);
        yield return new WaitForSeconds(2);
        tmpWave = StartCoroutine(TmpWaveSystem());

    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.2f);
        SetupLevelSpawnPoints();
        tmpWave = StartCoroutine(TmpWaveSystem());
        yield break;
    }

    IEnumerator TmpWaveSystem()
    {
        Quaternion spawnRot = Quaternion.Euler(new Vector3(-90, 0, 0));

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.2f);
            Instantiate(obsticleType1, Vector3.zero, spawnRot, this.gameObject.transform);
        }

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.2f);
            Instantiate(obsticleType1, spawnPointCL, spawnRot, this.gameObject.transform);
        }
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.2f);
            Instantiate(obsticleType1, spawnPointCR, spawnRot, this.gameObject.transform);
        }
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.2f);
            Instantiate(obsticleType1, spawnPointTL, spawnRot, this.gameObject.transform);
            Instantiate(obsticleType1, spawnPointBR, spawnRot, this.gameObject.transform);
        }
        yield return new WaitForSeconds(10);
        base.AcknowledgeLevelCompletion();
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

    void WaveAdvance()
    {

    }


    // Update is called once per frame
    void Update()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.scaledPixelWidth, Camera.main.scaledPixelHeight, cameraController.offset.z));
    }
}


public class Obsticle {

    public GameObject instance;
    Vector3 spawnPosition;
    bool spawned;
    bool destroyed;
}

public class ObsticleWave
{
    Obsticle[] wave;
    int count;
    int eliminated;
}

public class Section
{

}



