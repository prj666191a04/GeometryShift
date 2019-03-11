//Author Atilla puskas
//Description: script for a specific level


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelS1 : LevelBase
{

    public LevelOverlayUI levelUi;


    public GameObject obsticleType1;

    public float progress = 0;

    public GameObject pointTest;
    public GameObject mapFlow;
    Camera mainCam;
    private Coroutine tmpWave;

    LevelInit init;
    CameraControllerA cameraController;
    public Vector3 screenBounds;

    GameObject spawnPointBL;
    GameObject spawnPointBR;
    GameObject spawnPointCL;
    GameObject spawnPointCR;
    GameObject spawnPointTL;
    GameObject spawnPointTR;
    GameObject spawnPointCC;

    private void OnEnable()
    {
        CStatus.OnPlayerDeath += ResetLevel;
        LevelOverlayUI.OnIntroFinished += StartLevel;
    }

    private void OnDisable()
    {
        CStatus.OnPlayerDeath -= ResetLevel;
        LevelOverlayUI.OnIntroFinished -= StartLevel;
    }

    private void ResetLevel(int method = 0)
    {
        StopCoroutine(tmpWave);
        StartCoroutine(DelayedRestart());
    }


    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        mapFlow = GameObject.Find("MapFlow");
        init = GetComponent<LevelInit>();
        Debug.Log("levelS1");
        cameraController = Camera.main.GetComponent<CameraControllerA>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(mainCam.scaledPixelWidth, mainCam.scaledPixelHeight, cameraController.offset.z));
        StartCoroutine(LateStart()); 
    }

    void StartLevel()
    {
       tmpWave = StartCoroutine(TmpWaveSystem());
    }

    IEnumerator DelayedRestart()
    {
        yield return new WaitForSeconds(10);
        GeometryShift.playerStatus.gameObject.GetComponent<CController>().Respawn(init.spawnPoint.position);
        yield return new WaitForSeconds(2);
        tmpWave = StartCoroutine(TmpWaveSystem());

    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.2f);
        SetupLevelSpawnPoints();
        levelUi.PlayIntro();
        yield break;
    }

    IEnumerator TmpWaveSystem()
    {
        Quaternion spawnRot = Quaternion.Euler(new Vector3(-90, 0, 0));

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.2f);
            Instantiate(obsticleType1, spawnPointCC.transform.position, spawnRot, mapFlow.transform);
        }

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.2f);
            Instantiate(obsticleType1, spawnPointCL.transform.position, spawnRot, mapFlow.transform);
        }
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.2f);
            Instantiate(obsticleType1, spawnPointCR.transform.position, spawnRot, mapFlow.transform);
        }
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.2f);
            Instantiate(obsticleType1, spawnPointTL.transform.position, spawnRot, mapFlow.transform);
            Instantiate(obsticleType1, spawnPointBR.transform.position, spawnRot, mapFlow.transform);
        }
        yield return new WaitForSeconds(10);
        base.AcknowledgeLevelCompletion();
        yield break;

        

    }
    void SetupLevelSpawnPoints()
    {
       Vector3 BL = new Vector3(screenBounds.x/2, screenBounds.y/2, 0);
       Vector3 BR = new Vector3((screenBounds.x * -1) / 2, screenBounds.y / 2, 0);
       Vector3 CL = new Vector3(screenBounds.x/2, 0, 0);
       Vector3 CR = new Vector3((screenBounds.x * -1) / 2, 0 / 2, 0);
       Vector3 TL = new Vector3(screenBounds.x / 2, (screenBounds.y * -1) / 2, 0);
       Vector3 TR = new Vector3((screenBounds.x * -1) / 2, (screenBounds.y * -1) / 2, 0);
        

        spawnPointBL = Instantiate(pointTest, BL , Quaternion.identity, mapFlow.transform);
        spawnPointBR = Instantiate(pointTest, BR , Quaternion.identity, mapFlow.transform);
        spawnPointCL = Instantiate(pointTest, CL , Quaternion.identity, mapFlow.transform);
        spawnPointCR = Instantiate(pointTest, CR , Quaternion.identity, mapFlow.transform);
        spawnPointTL = Instantiate(pointTest, TL , Quaternion.identity, mapFlow.transform);
        spawnPointTR = Instantiate(pointTest, TR , Quaternion.identity, mapFlow.transform);
        spawnPointCC = Instantiate(pointTest, Vector3.zero, Quaternion.identity, mapFlow.transform);
    }

    void WaveAdvance()
    {

    }


    // Update is called once per frame
    void Update()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(mainCam.scaledPixelWidth, mainCam.scaledPixelHeight, cameraController.offset.z));
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



