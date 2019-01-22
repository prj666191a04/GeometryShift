using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class CConfig : MonoBehaviour
{
    public abstract void SetupCharacter(GameObject playerPrefab, Transform spawnPoint, GameObject parentObject);
   
}
