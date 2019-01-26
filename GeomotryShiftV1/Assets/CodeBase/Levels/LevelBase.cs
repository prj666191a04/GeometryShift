using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBase : MonoBehaviour
{

    public static LevelBase instance;
    
    public delegate void LevelMessage(int id, int code);
    public delegate void LevelEvent();
    public static event LevelMessage OnLevelCompleeted;
    public static event LevelEvent OnLevelFailed;
    public int levelId_ = 0;



    private void Awake()
    {
        instance = this;
    }

    //id is the id number of the level, code is the return value for how the level was compleeted, return 0 if no special conditions exist
    public void AcknowledgeLevelCompletion(int code = 0)
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
    protected void TerminateLevelAttempt()
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


}
