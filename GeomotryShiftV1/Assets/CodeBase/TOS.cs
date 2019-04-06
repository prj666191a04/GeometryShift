using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TOS : MonoBehaviour
{

    public Button acceptBtn, declineBtn;

    void Start()
    {
        acceptBtn.onClick.AddListener(AcceptTOS);
        declineBtn.onClick.AddListener(DeclineTOS);
    }

    void AcceptTOS()
    {
        Debug.Log("Accept TOS");
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameTestA");
    }

    void DeclineTOS()
    {
        Debug.Log("Declined TOS");
        Application.Quit();
    }

}
