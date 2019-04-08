using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TOS : MonoBehaviour
{

    public Button acceptBtn, declineBtn;

    void Start()
    {
        if (PlayerPrefs.GetInt("TOS") == 1)
        {
            SceneManager.LoadScene("GameTestA");
            Debug.Log("Tos allready accepted");
        }
       // acceptBtn.onClick.AddListener(AcceptTOS);
       // declineBtn.onClick.AddListener(DeclineTOS);
    }

    public void AcceptTOS()
    {
        PlayerPrefs.SetInt("TOS", 1); // Sets 1 for yes
        Debug.Log("Accept TOS");
        SceneManager.LoadScene("GameTestA");
    }

    public void DeclineTOS()
    {
        PlayerPrefs.SetInt("TOS", 0); // Sets 0 for no
        Debug.Log("Declined TOS");
        Application.Quit();
    }

}
