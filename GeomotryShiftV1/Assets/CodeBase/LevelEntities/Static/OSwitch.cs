using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSwitch : MonoBehaviour
{
    public int myId;
    public List<GameObject> groupOne;
    public List<GameObject> groupTwo;
    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        DefenceTerminal.OnTerminalMessage += Toggle;
        LevelBase.OnLevelReset += ResetSwitch;
    }
    private void OnDisable()
    {
        DefenceTerminal.OnTerminalMessage -= Toggle;
        LevelBase.OnLevelReset -= ResetSwitch;
    }

    private void ResetSwitch()
    {
        active = false;
        foreach (GameObject g in groupOne)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in groupTwo)
        {
            g.SetActive(true);
        }
    }

    void Toggle(int id)
    {
        if(id == myId)
        {
            active = !active;
            if(active)
            {
                foreach(GameObject g in groupOne)
                {
                    g.SetActive(true);
                }
                foreach (GameObject g in groupTwo)
                {
                    g.SetActive(false);
                }
            }
            else
            {
                foreach (GameObject g in groupOne)
                {
                    g.SetActive(false);
                }
                foreach (GameObject g in groupTwo)
                {
                    g.SetActive(true);
                }
            }
        }
    }
}
