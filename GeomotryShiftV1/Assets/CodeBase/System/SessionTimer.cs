//Author Atilla puskas
//Description: keeps track of play time

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionTimer : MonoBehaviour
{
    public long SessionTime;
    public float currentSessionTime = 0f;
    // Start is called before the first frame update
    public void Init()
    {
        SessionTime = LevelLoader.instance.dataCore.groupedData.playerData.playTime;
    }
    private void OnDisable()
    {
       SessionTime = 0;
       currentSessionTime = 0;
    }
    // Update is called once per frame
    void Update()
    {
        currentSessionTime += Time.unscaledDeltaTime;
    }

    public long EndSession()
    {
        int roundedSessionTime = Mathf.RoundToInt(currentSessionTime);
        return SessionTime + roundedSessionTime;
    }

}
