using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public string playerName;
    public float highScore;
    public static Title Instance;
    public Text scoreText;
    public GameObject titleScore;
    public InputField nameInput;

    // Start is called before the first frame update

    void Start()
    {
       

    }
    private void Awake()
    {
        
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
       // DontDestroyOnLoad(titleScore);
        LoadHighScore();
    }
    private void Update()
    {

        
        SaveName();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {playerName = nameInput.text;
            scoreText.text = "High Score: " + highScore;
            
        }
        else
        {
            
        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
        
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public float highScore;
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

           highScore = data.highScore;
        }
       
    }
    public void LoadPlayerName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
        }
    }
}
