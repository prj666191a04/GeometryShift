//Author Atilla Puskas
//Desc Blocks a section of the map if specified levelID is not compleeted

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBlockage : MonoBehaviour
{
    public int levelId;
    bool compleete = false;

    private Color targetColor;
    private Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        targetColor = mat.color;
        if (LevelLoader.instance.GetDataCore().groupedData.worldState.levelState[levelId].GetCompleetedCode() >= 0)
        {
            Debug.Log(LevelLoader.instance.GetDataCore().groupedData.worldState.levelState[levelId].GetCompleetedCode());
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (mat.color != targetColor)
        {
            mat.color = Color.Lerp(mat.color, targetColor, Time.deltaTime * 3);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mat.color = Color.red;
        }
    }


}
