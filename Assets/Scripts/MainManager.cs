using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public Text bestScoreText;
    public int bestScore;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    public void Awake()
    {
        // First check that the MainManagerMenu's instance is null or not. This is useful when we directly run the Main scene, rather than coming from Menu Scene.
        if (MainManagerMenu.Instance != null)
        {
            // whenever this Main scene is loaded, it will display the last best score saved & the player name whose that best score is, is loaded.
            bestScoreText.text = "Best Score: " + MainManagerMenu.Instance.LoadPlayerName() + " : " + MainManagerMenu.Instance.LoadBestScore();
        }
    }

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
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    // we will save the best score if it is less than the current score, in JSON format, so therefore we call SaveBestScoreandName() of "MainManagerMenu.cs"
    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        if (m_Points >= MainManagerMenu.Instance.LoadBestScore()) // check whether the current score is greater than the lastly saved best score.
        {
            bestScore = m_Points;
            bestScoreText.text = "Best Score: " + MainManagerMenu.Instance.textInputEntered + " : " + bestScore;
            MainManagerMenu.Instance.SaveBestScoreAndName(bestScore);
        }
           
    }
}
