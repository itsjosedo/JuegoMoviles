using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 

    
    public int playerLives = 3;
    public int playerScore = 0;

    public Text scoreText;
    public Text livesText;

    void Awake()
    {
        // Configurar Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persiste entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();
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
        Debug.Log("Game Over!");
        
    }
}
