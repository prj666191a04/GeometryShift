//Author Atilla Puskas
//Desc Tile that kills the player if stood on for to long;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTile : MonoBehaviour
{
    public float maxTime = 3f;
    public float currentTime;
    float tickAmmount;
    Material mat;
    public Color DangerColor;
    Color startColor;
    int deductRate;

    public bool rengeneritive;


    private void OnEnable()
    {
        LevelBase.OnLevelReset += Setup;
    }

    private void OnDisable()
    {
        LevelBase.OnLevelReset -= Setup;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        tickAmmount = 1 / maxTime;
        mat = GetComponent<MeshRenderer>().material;
        startColor = mat.color;
        if (rengeneritive)
            deductRate = 2;
        else
            deductRate = 1;
    }

    void Setup()
    {
        currentTime = 0;
        tickAmmount = 1 / maxTime;
        mat.color = startColor;
    }

    private void Update()
    {
        if(rengeneritive && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
                currentTime = 0;
            mat.color = Color.Lerp(startColor, DangerColor, currentTime / maxTime);
        }
    }


    private void OnCollisionStay(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            currentTime += Time.deltaTime * deductRate;
           
            if(currentTime > maxTime)
            {
                GeometryShift.playerStatus.AbsoluteDamage(9999f);
                currentTime = maxTime;
                
            }
            mat.color = Color.Lerp(startColor, DangerColor, currentTime / maxTime);
        }
    }
   
}
