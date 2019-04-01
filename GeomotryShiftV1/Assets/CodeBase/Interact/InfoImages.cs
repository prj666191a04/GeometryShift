//Author Atilla Puskas
//Description Stores images for help screens

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoImages : MonoBehaviour
{
    public Sprite[] images;
    public Image mainImage;
    // Start is called before the first frame update
    public void SetImage(int i)
    {
        mainImage.sprite = images[i];
    }
}
