//Author AtillaPuskas
//Description controls video and audio settings for the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.Audio;

public class SettingMenueV2 : MonoBehaviour
{
    public Dropdown resSelect;
    public Dropdown qSelect;
    public Dropdown dSelect;
    public Toggle vSyncToggle;

    public Slider masterVolumeSlider;
    public Slider effectsVolumeSlider;
    public Slider guiVolumeSlider;
    public Slider musicVolumeSlider;

    GameConfig config;    
    [SerializeField]
    public Resolution[] resolutions;
    public string[] resolutionStrings;
    string[] qStrings;
    string dataPath;

    public bool MM = false;

    void OnEnable()
    {
        CheckConfig();
        Setup();
    }
    void CheckConfig()
    {
        dataPath = Application.dataPath + "/Config";
        if(!Directory.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
        }
        if(File.Exists(dataPath + "/config.cnf"))
        {
            string file = File.ReadAllText(dataPath + "/config.cnf");
            config = JsonUtility.FromJson<GameConfig>(file);
        }
        else
        {
            config = new GameConfig(Screen.currentResolution.width, Screen.currentResolution.height, Screen.currentResolution.refreshRate);
            Debug.Log("making new config file");
        }
    }

    public void SaveConfig()
    {
        string file = JsonUtility.ToJson(config);
        File.WriteAllText(dataPath + "/config.cnf", file);
        SystemSounds.instance.UIAdavance();
        if(MM)
        {
            GeometryShift.instance.StateChange(GeometryShift.SystemState.MainMenue);
        }

    }

    public static void LoadSavedSettings()
    {
        if (File.Exists(Application.dataPath + "/Config" + "/config.cnf"))
        {
            Debug.Log("loading system settings");
            string file = File.ReadAllText(Application.dataPath + "/Config" + "/config.cnf");
            GameConfig loadedSettings = JsonUtility.FromJson<GameConfig>(file);
            GeometryShift.instance.mainMixer.SetFloat("masterVolume", loadedSettings.masterVolume);
            GeometryShift.instance.mainMixer.SetFloat("effectsVolume", loadedSettings.effectsVolume);
            GeometryShift.instance.mainMixer.SetFloat("uiVolume", loadedSettings.guiVolume);
            GeometryShift.instance.mainMixer.SetFloat("musicVolume", loadedSettings.musicVolume);
            if (loadedSettings.vSync)
            {
                QualitySettings.vSyncCount = 1;
            }
            else
            {
                QualitySettings.vSyncCount = 0;
            }
            //QualitySettings.SetQualityLevel(loadedSettings.qualityLevel);
            //Screen.SetResolution(loadedSettings.ScreenWidth, loadedSettings.ScreenHeight, Screen.fullScreen, loadedSettings.refreshRate);

            switch (loadedSettings.fullScreenMode)
            {
                case 0:
                    Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                    break;
                case 1:
                    Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                    break;
                case 2:
                    Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                    break;
                case 3:
                    Screen.fullScreenMode = FullScreenMode.Windowed;
                    break;
            }


        }
    }

    public void Setup()
    {
        Debug.Log("running setup for settings menue");
        if(QualitySettings.vSyncCount != 0)
        {
            vSyncToggle.isOn = true;
        }
        else
        {
            vSyncToggle.isOn = false;
        }

        dSelect.value = (int)Screen.fullScreenMode;
        dSelect.RefreshShownValue();
        //var resolutions_ = Screen.resolutions.Where(res => res.refreshRate == 60);
        resolutions = Screen.resolutions;
        resolutionStrings = new string[resolutions.Length];
        int resIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionStrings[i] = resolutions[i].width.ToString() + "x" + resolutions[i].height.ToString() + " :" + resolutions[i].refreshRate.ToString() + "hz";

            //if(config != null)
            //{
            //    if (resolutions[i].width == config.ScreenWidth && resolutions[i].height == config.ScreenHeight)
            //    {
            //        resIndex = i;
            //    }
            //}
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                resIndex = i;
            }
        }
        resSelect.options.Clear();
        List<string> rOptions = new List<string>();
        foreach(string s in resolutionStrings)
        {
            rOptions.Add(s);
        }
        resSelect.AddOptions(rOptions);
        resSelect.value = resIndex;
        resSelect.RefreshShownValue();

        int qIndex = QualitySettings.GetQualityLevel();
        qStrings = QualitySettings.names;

        List<string> qOptions = new List<string>();
        foreach(string s in qStrings)
        {
            qOptions.Add(s);
        }
        qSelect.options.Clear();
        qSelect.AddOptions(qOptions);
        qSelect.value = qIndex;

        float tmpValue = 0;

        GeometryShift.instance.mainMixer.GetFloat("masterVolume", out tmpValue);
        masterVolumeSlider.value = tmpValue;
        GeometryShift.instance.mainMixer.GetFloat("effectsVolume", out tmpValue);
        effectsVolumeSlider.value = tmpValue;
        GeometryShift.instance.mainMixer.GetFloat("uiVolume", out tmpValue);
        guiVolumeSlider.value = tmpValue;
        GeometryShift.instance.mainMixer.GetFloat("musicVolume", out tmpValue);
        musicVolumeSlider.value = tmpValue;

    }

    public void SetMasterVolume(float volume)
    {
        GeometryShift.instance.mainMixer.SetFloat("masterVolume", volume);
        config.masterVolume = volume;
    }
    public void SetEffectsVolume(float volume)
    {
        GeometryShift.instance.mainMixer.SetFloat("effectsVolume", volume);
        config.effectsVolume = volume;
    }
    public void SetGUIVolume(float volume)
    {
        GeometryShift.instance.mainMixer.SetFloat("uiVolume", volume);
        config.guiVolume = volume;
    }
    public void SetMusicVolume(float volume)
    {
        GeometryShift.instance.mainMixer.SetFloat("musicVolume", volume);
        config.musicVolume = volume;
    }

    public void SetVsync(bool vSync)
    {
        if(vSync)
        {
            QualitySettings.vSyncCount = 1;
            config.vSync = true;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            config.vSync = false;
        }
        
    }
    public void SetQuality(int qIn)
    {
        QualitySettings.SetQualityLevel(qIn);
        config.qualityLevel = qIn;
    }
    public void SetResolution(int resIndex)
    {
        Screen.SetResolution(resolutions[resIndex].width, resolutions[resIndex].height, Screen.fullScreen, resolutions[resIndex].refreshRate);
        config.ScreenWidth = resolutions[resIndex].width;
        config.ScreenHeight = resolutions[resIndex].height;
    }
    public void SetFullScreen(int fullScreen)
    {
        config.fullScreenMode = fullScreen;
        switch (fullScreen) {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
            case 3:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }
   
}
[System.Serializable]
public class GameConfig {
    [SerializeField]
    public int ScreenWidth;
    [SerializeField]
    public int ScreenHeight;
    [SerializeField]
    public int fullScreenMode;
    [SerializeField]
    public bool vSync;
    [SerializeField]
    public int qualityLevel;
    [SerializeField]
    public int targetFrameRate;
    [SerializeField]
    public int refreshRate;
    [SerializeField]
    public float masterVolume;
    [SerializeField]
    public float effectsVolume;
    [SerializeField]
    public float guiVolume;
    [SerializeField]
    public float musicVolume;


    public GameConfig(int w, int h, int r)
    {
        ScreenWidth = w;
        ScreenHeight = h;
        fullScreenMode = 0;
        vSync = true;
        qualityLevel = QualitySettings.GetQualityLevel();
        targetFrameRate = 60;
        refreshRate = r;
        masterVolume = 0;
        effectsVolume = 0;
        guiVolume = 0;
        musicVolume = 0;
    }

}
    
