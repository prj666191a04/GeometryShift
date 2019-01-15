using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveSystem : MonoBehaviour
{
    private string fileName = "TestSaveData.json";
    private string filePath;
    public Button saveButton, loadButton;
    public GameObject player;
    public List<GameObject> savedObjects;
    public GameData gameData;

    public static SaveSystem _instance;

    public SaveSystem Instance
    {
        get { return _instance; }
    }

    

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance== null)
        {
            _instance = new SaveSystem();
        }
        if (gameData == null)
        {
            gameData = new GameData();
        }
        filePath = Path.Combine(Application.dataPath, fileName);

    }

  
    void Start()
    {
        saveButton.onClick.AddListener(SaveGameData);
        loadButton.onClick.AddListener(LoadGameData);
        player = GameObject.FindGameObjectWithTag("Player");
        //SaveGameData();
        //LoadGameData();
        //Debug.Log(filePath);
    }

    public void SaveGameData()
    {

        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }

        string json = setUpSaveData();

        File.WriteAllText(filePath, json);
    }

    public void LoadGameData()
    {
        string json;
        
        if (File.Exists(filePath))
        {
            //player = GameObject.FindGameObjectWithTag("Player");
            json = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(json);

            //player Data
            player.transform.position = new Vector3 (gameData.playerX, gameData.playerY, gameData.playerZ);
            player.GetComponent<Renderer>().material.SetColor("_Color", gameData.playerColour);
            Debug.Log(gameData.playerX + " " + gameData.playerY + " " + gameData.playerZ);

            //Objects data
            for (int i = 0; i < gameData.objectData.Count; i++)
            {
                savedObjects[i].transform.position = new Vector3(gameData.objectData[i].objectX, gameData.objectData[i].objectY, gameData.objectData[i].objectZ);
                savedObjects[i].GetComponent<Renderer>().material.SetColor("_Color", gameData.objectData[i].objectColour);        
            }
        }
        else
        {
            Debug.Log("File is missing " + filePath);
        }
    }

    string setUpSaveData ()
    {

        gameData.saveName = "Test Save 1";
        gameData.playerX = player.transform.position.x;
        gameData.playerY = player.transform.position.y;
        gameData.playerZ = player.transform.position.z;
        gameData.playerColour = player.GetComponent<Renderer>().material.color;

        for (int i = 0;  i< savedObjects.Count; i++)
        {
            if (savedObjects.Count > gameData.objectData.Count)
                gameData.objectData.Add(new ObjectData());
            gameData.objectData[i].objectX = savedObjects[i].transform.position.x;
            gameData.objectData[i].objectY = savedObjects[i].transform.position.y;
            gameData.objectData[i].objectZ = savedObjects[i].transform.position.z;
            gameData.objectData[i].objectColour = savedObjects[i].GetComponent<Renderer>().material.color;

        }

        string jsonData = JsonUtility.ToJson(gameData);
        return jsonData;
     }

}

[System.Serializable]
public class GameData
{
    //Save file info
    public string saveName;
    
    //Player Info
    public float playerX;
    public float playerY;
    public float playerZ;
    public Color playerColour;
    //public float playerHealth;


    //Scene Info
    public List<ObjectData> objectData;

}

[System.Serializable]
public class ObjectData
{
    public float objectX;
    public float objectY;
    public float objectZ;
    public bool objectState;
    public Color objectColour;
}
