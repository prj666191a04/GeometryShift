using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CConfig : MonoBehaviour
{
    //temporary implementation of event, under consideration might be removed for preformance reasosns.
    public delegate void CConfigDel(GameObject playerObj);
    public static event CConfigDel OnPlayerReady;
    public virtual void SetupCharacter(GameObject playerPrefab, Transform spawnPoint, GameObject parentObject)
    {
        throw new NotImplementedException();
    }


    protected void PlayerReady(GameObject playerRef)
    {
        if (OnPlayerReady != null)
        {
            OnPlayerReady(playerRef);
        }
        else
        {
            Debug.LogWarning("CConfig/OnPlayerReady has no subscribers!");
        }
    }
   
}
