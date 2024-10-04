using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    const int k_startSceneIndex = 0;

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool m_started = false;
    private int m_points;
    
    private bool m_gameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        AddPoint(0); // no score points added, this is to update name in score string to use persisted name.

        BestScoreText.text = DataManager.Instance.BestScore.ToString();

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
                brick.OnDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(k_startSceneIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_points += point;
        ScoreText.text = $"{DataManager.Instance.Name} Score : {m_points}";
    }

    public void GameOver()
    {
        m_gameOver = true;
        GameOverText.SetActive(true);

        if (m_points > DataManager.Instance.BestScore.Score)
        {
            DataManager.Instance.BestScore.Name = DataManager.Instance.Name;
            DataManager.Instance.BestScore.Score = m_points;
            DataManager.Instance.SaveBestScore();
        }
    }
}
