using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public static float volumeMultiplier = -1f;
    static private Slider x;

    public void Start()
    {
        x = GetComponent<Slider>();
        LoadFromFile();
    }

    public static float GetVolumeMultiplier()
    {
        if (volumeMultiplier < -0.5f)
        {
            volumeMultiplier = 0f;
            StreamReader reader = new StreamReader("volume.txt");
            volumeMultiplier = float.Parse(reader.ReadLine());
            reader.Close();
        }
        return volumeMultiplier;
    }
    public void SetVolumeMultiplier(float newMult)
    {
        volumeMultiplier = newMult;
    }

    public void SaveToFile()
    {
        StreamWriter writer = new StreamWriter("volume.txt", false);
        writer.WriteLine(x.value);
        writer.Close();
    }

    public static void LoadFromFile()
    {
        x.value = 0f;
        StreamReader reader = new StreamReader("volume.txt");
        x.value = float.Parse(reader.ReadLine());
        reader.Close();
    }



    public void OnDestroy()
    {
        volumeMultiplier = x.value;
        SaveToFile();
    }

}
