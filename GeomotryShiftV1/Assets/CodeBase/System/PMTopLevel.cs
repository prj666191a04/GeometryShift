using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMTopLevel : MonoBehaviour
{

    public static GameObject loadedSubMenue;
    public GameObject menueObject;
    // Start is called before the first frame update
    public void Apear()
    {
        menueObject.SetActive(true);        
    }
    public void Hide()
    {
        menueObject.SetActive(false);
    }

    public void Toggle(bool active)
    {

        menueObject.SetActive(active);
    }


}
