//Author Atilla Puskas
//Description, Controls the usage of the recovery item in level

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecoveryActiveSlot : ConsumeableActiveSlot
{
    public Text counter;
    public Image icon1;
    public Image hpIndication;
    public Image cooldownIndication;
    public int coolDownTime = 3;
    float timeUsed;
    Color targetColor;
    private void OnEnable()
    {
        timeUsed = Time.time;
        qty_ = LevelLoader.instance.dataCore.groupedData.playerData.inventory_.recoverys_.qty_;
        UpdateCounter();
        targetColor = counter.color;
    }

    private void Update()
    {
        GetFillAmmount();
        if (counter.color != targetColor)
        {
            counter.color = Color.Lerp(counter.color, targetColor, Time.deltaTime * 3);
        }
    }
    void saveUsage()
    {
        LevelLoader.instance.dataCore.groupedData.playerData.inventory_.recoverys_.Consume(1);
        LevelLoader.instance.SoftAutoSave();
    }

    public override void Activate()
    {
        
        if(qty_ > 0 && Time.time - timeUsed >= coolDownTime)
        {
            timeUsed = Time.time;
            Debug.Log("recovery activated");
            GeometryShift.playerStatus.RecoverItem();
            qty_--;
            UpdateCounter();
            saveUsage();
        }
        else if(qty_ <= 0)
        {
            counter.color = Color.red;   
        }
    }

    void GetFillAmmount()
    {
        float timeSinceLastUse = Time.time - timeUsed;
        if(timeSinceLastUse > 0)
        {
            cooldownIndication.fillAmount = timeSinceLastUse / coolDownTime;
        }
    }

    void UpdateCounter()
    {
        if(qty_ > 9)
        {
            counter.text = qty_.ToString();
        }
        else
        {
            counter.text = "0" + qty_.ToString();
        }
    }
}
