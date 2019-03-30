//Ahuthor Atilla Puskas
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelT5ResetScript : MonoBehaviour
{
    private void OnEnable()
    {
        LevelBase.OnLevelReset += ReconfigureCharacter;
    }
    private void OnDisable()
    {
        LevelBase.OnLevelReset -= ReconfigureCharacter;
    }

    void ReconfigureCharacter()
    {
        GeometryShift.playerStatus.gameObject.GetComponent<CController>().ChangeMotor(0);
        GeometryShift.instance.cameraController.Init(new Vector3(0f, 13f, -6f));
        GeometryShift.instance.cameraController.LookAt(new Vector3(68.931f, 0f, 0f));
        GeometryShift.instance.cameraController.borderDistance = 4f;
    }

}
