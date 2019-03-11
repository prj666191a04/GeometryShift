//Author Atilla puskas
//Description: Controls for the interaction related UI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    public GameObject InteractionRoot;
    public Text subText;
    // Start is called before the first frame update

    public void Apear(string text)
    {
        subText.text = text;
        InteractionRoot.SetActive(true);
    }
    public void Hide()
    {
        InteractionRoot.SetActive(false);
    }
}
