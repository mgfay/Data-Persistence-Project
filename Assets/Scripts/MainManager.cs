using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private float m_Points;
    
    private bool m_GameOver = false;
    public Text disName;
    public Text disScore;
    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        disName.text = "Name: " + Title.Instance.playerName;
        disScore.text = "Best Score: " + Title.Instance.highScore;
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        if (m_Points > Title.Instance.highScore)
        {
            Title.Instance.highScore = m_Points;



        }
    }

    public void GameOver()
    {
        Title.Instance.SaveHighScore();
        Title.Instance.SaveName();
        m_GameOver = true;
        GameOverText.SetActive(true);
        
        
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        Title.Instance.SaveName();
        Title.Instance.SaveHighScore();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
    public void SaveNameEntered()
    {
        Title.Instance.SaveName();
    }

    public void LoadNameEntered()
    {
        Title.Instance.LoadPlayerName();
        
    }
    public void SaveScore()
    {
        Title.Instance.SaveHighScore();
    }

    public void LoadScore()
    {
        Title.Instance.LoadHighScore();
      
    }
}
