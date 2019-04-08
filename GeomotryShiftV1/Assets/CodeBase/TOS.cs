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
        if (PlayerPrefs.GetInt("TOSAccept") == 1)
        {
            SceneManager.LoadScene("GameTestA");
        }
        acceptBtn.onClick.AddListener(AcceptTOS);
        declineBtn.onClick.AddListener(DeclineTOS);
    }

    void AcceptTOS()
    {
        PlayerPrefs.SetInt("TOSAccept", 1); // Sets 1 for yes
        Debug.Log("Accept TOS");
        SceneManager.LoadScene("GameTestA");
    }

    void DeclineTOS()
    {
        PlayerPrefs.SetInt("TOSAccept", 0); // Sets 0 for no
        Debug.Log("Declined TOS");
        Application.Quit();
    }

}
