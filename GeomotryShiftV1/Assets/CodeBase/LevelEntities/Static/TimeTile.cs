//Author Atilla Puskas
//Desc Tile that kills the player if stood on for to long;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTile : MonoBehaviour
{
    public float maxTime = 3f;
    float currentTime;
    float tickAmmount;
    Material mat;
    public Color DangerColor;
    Color startColor;

    public bool rengeneritive;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        tickAmmount = 1 / maxTime;
        mat = GetComponent<MeshRenderer>().material;
        startColor = mat.color;
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            currentTime += Time.deltaTime;
           
            if(currentTime > maxTime)
            {
                GeometryShift.playerStatus.AbsoluteDamage(9999f);
                currentTime = maxTime;
                
            }
            mat.color = Color.Lerp(startColor, DangerColor, currentTime / maxTime);
        }
    }

    
}
