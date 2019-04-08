//Author Atilla puskas
//Description: A main script to intilize the game and keep track of certin values for fast acsses from other scripts
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;


//INPORTANT values ending with Prefab Should not be modified at runtime.

public class GeometryShift : MonoBehaviour
{

    public static Camera mainCamera;

    public CameraControllerA cameraController;

    public static GeometryShift instance;

    public static CStatus playerStatus;

    public PMTopLevel pauseMenue;
    public bool pauseMenueActive = false;

    public SessionTimer sessionTimer;

    private string dataPath;

    public GameObject consumablePanel;
    public AudioMixer mainMixer;

    public GameObject infoPanel;
    public InfoImages inImages;
    GameObject loadedInfoSet;

    bool infoInterupt = false;


    private static SystemState systemState = SystemState.MainMenue;
    public enum SystemState
    {
        MainMenue,
        WorldMap,
        InLevel,
        Loading
    }

    //UI- this code might need to be moved to a ui manager class later on
    public Transform interactionUIContainer;
    public Transform activeUIContainer;

    public GameObject mainMenuePrefab;
    public GameObject openWorldUiPrefab;

    public GameObject loadedUiSet;

    public InteractionUI interactionUI;

    public void DspInfo(int id)
    {
        infoInterupt = true;
        inImages.SetImage(id);
        infoPanel.SetActive(true);
        GeometryShift.playerStatus.GetComponent<CController>().DisableMovement();
    }

    public static void AwardRecoveryItem (int ammount)
    {
        LevelLoader.instance.dataCore.groupedData.playerData.inventory_.recoverys_.Add(ammount);
    }
    public void StartSessionTimer()
    {
        sessionTimer.enabled = true;
    }
    public void StopSessionTimer()
    {
        sessionTimer.enabled = false;
    }

    public string GetDataPath()
    {
        return dataPath;
    }

    public static SystemState GetSystemState()
    {
        return systemState;
    }

    public void StateChange(SystemState state)
    {
        SystemState prevoiusState = systemState;
        systemState = state;

        
        switch (state) {

            case SystemState.Loading:
                DistroyLoadedUISet();
                consumablePanel.SetActive(false);
                //TODO: Display Loading screen (does not yet exist)
                break;
            case SystemState.MainMenue:
                DistroyLoadedUISet();
                loadedUiSet = Instantiate(mainMenuePrefab, activeUIContainer);
                consumablePanel.SetActive(false);
                break;
            case SystemState.WorldMap:
                DistroyLoadedUISet();
                consumablePanel.SetActive(false);
                break;
            case SystemState.InLevel:
                DistroyLoadedUISet();
                consumablePanel.SetActive(true);
                break;
        }

    }

    private void ChangeUISet( GameObject UISet)
    {

        DistroyLoadedUISet();
        Instantiate(UISet, activeUIContainer);
        
    }
    public void DistroyLoadedUISet()
    {
        if(loadedUiSet != null)
        {
            Destroy(loadedUiSet);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        Initalize();
    }

    private void Update()
    {
        SystemInput();
    }


    private void SaveFileCheck()
    {
        dataPath = Application.dataPath + "/DataCore";
        if (!Directory.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
        }
        for (int i = 0; i < 3; i++)
        {
            if (!File.Exists(dataPath + "/slot" + i.ToString() + ".save"))
            {
                File.Create(dataPath + "/slot" + i.ToString() + ".save");
            }
        }

    }



    //Starts the game
    private void Initalize()
    {
        SettingMenueV2.LoadSavedSettings();
        SaveFileCheck();
        loadedUiSet = Instantiate(mainMenuePrefab, activeUIContainer);



    }
    public void QuitGame()
    {
        Application.Quit();
    }


    private void SystemInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !infoInterupt)
        {
            if (systemState == SystemState.InLevel || systemState == SystemState.WorldMap )
            {
                pauseMenueActive = !pauseMenueActive;

                if (pauseMenueActive)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;

                pauseMenue.Toggle(pauseMenueActive);
            }
            else
            {
                pauseMenue.Hide();
                pauseMenueActive = false;

            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            infoPanel.SetActive(false);
            infoInterupt = false;
            GeometryShift.playerStatus.GetComponent<CController>().EnableMovement();
        }
                 

    }


}
