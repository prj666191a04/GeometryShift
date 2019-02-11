using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenue : MonoBehaviour
{
    public void LoadWorld()
    {
        LevelLoader.instance.LoadWorldMap();
    }

    public void QuitGame()
    {
        GeometryShift.instance.QuitGame();
    }
}

