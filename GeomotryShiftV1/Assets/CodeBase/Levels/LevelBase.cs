//Author Atilla puskas
//Description: Base level class


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBase : MonoBehaviour
{

    public LayerMask layerSet0;
    public LayerMask layerSet1;
    public LayerMask layerSet2;
    public LayerMask layerSet3;

    public static LevelBase instance;
    
    public delegate void LevelMessage(int id, int code);
    public delegate void LevelEvent();
    public static event LevelMessage OnLevelCompleeted;
    public static event LevelEvent OnLevelFailed;
    public static event LevelEvent OnLevelReset;
    public static event LevelEvent OnLevelRessultScreen;
    public int levelId_ = 0;
    public LevelInit init_;
    public string levelName_ = "";



    private void Awake()
    {
        instance = this;
    }
    public void LevelRessultScreenSendMessage()
    {
        if(OnLevelRessultScreen != null)
        {
            OnLevelRessultScreen();
        }
    }

    //id is the id number of the level, code is the return value for how the level was compleeted, return 0 if no special conditions exist
    public virtual void AcknowledgeLevelCompletion(int code = 0)
    {
        if(OnLevelCompleeted != null)
        {
            OnLevelCompleeted(levelId_, code);
        }
        else
        {
            Debug.LogError("LevelBase.cs: On Level Compleeted has no subscribers!");
        }
    }
    public virtual void TerminateLevelAttempt()
    {
        if(OnLevelFailed != null)
        {
            OnLevelFailed();
        }
        else
        {
            Debug.LogError("LevelBase.cs: On Level Failed has no subscribers!");
        }
    }

    public void LevelResetSendMessage()
    {
        if(OnLevelReset != null)
        {
            OnLevelReset();
        }
    }

}
