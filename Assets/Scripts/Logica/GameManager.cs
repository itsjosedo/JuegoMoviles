using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private float timetoWin;
    private float timeless;

    [Header("Boss")]
    [SerializeField] private GameObject Boss;

    [Header("vida")]
    public int playerLives = 3;
    public int playerScore = 0;

    [Header("caracteristicas")]
    public Text scoreText;
    public Text livesText;
    public GameObject gameOverPanel;
    public GameObject GameWinPanel;
    private bool isGameOver = false;

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        timetoWin = FindAnyObjectByType<TimeToWin>().time;
        UpdateUI();
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
            
    }
    private void Update()
    {
        timeless = FindAnyObjectByType<TimeToWin>().time;
        if (timeless < timetoWin / 2)
        {
            Boss.SetActive(true);
        }
    }

    public void AddScore(int points)
    {
        playerScore += points;
        UpdateUI();
    }

    
    public void LoseLife()
    {
        playerLives--;
        UpdateUI();

        if (playerLives <= 0)
        {
            GameOver();
        }
    }

    
    public void AddLife(int amount)
    {
        playerLives += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + playerScore;
        if (livesText != null) livesText.text = "Vidas: " + playerLives;
    }

    void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("Game Over!");

        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        Time.timeScale = 0f;

    }

    public void GameWin()
    {
        GameWinPanel.SetActive(true);
        Time.timeScale = 0f;

    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game level 1");
    }
}
