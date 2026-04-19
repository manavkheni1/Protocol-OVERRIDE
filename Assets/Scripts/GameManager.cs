using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 
using TMPro; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public float timer = 0f;
    public bool isGameOver = false;

    // The hidden variable to hold the record
    private int highScore = 0;

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI timeText;  
    public TextMeshProUGUI highScoreText; // 1. ADDED THE NEW SLOT
    public GameObject gameOverPanel;

    void Awake()
    {
        instance = this; 
    }

    void Start()
    {
        // 2. LOAD THE SAVED HIGH SCORE WHEN THE GAME BOOTS UP
        highScore = PlayerPrefs.GetInt("BestScore", 0); 
        
        if (highScoreText != null) 
        {
            highScoreText.text = "HIGH SCORE: " + highScore;
        }
    }

    void Update()
    {
        if (!isGameOver)
        {
            timer += Time.deltaTime;
            
            if (scoreText != null) scoreText.text = "CORRUPTIONS CLEARED: " + score;
            if (timeText != null) timeText.text = "UPTIME: " + Mathf.Round(timer) + "s";
        }
    }

    public void AddScore(int points)
    {
        if (!isGameOver) 
        {
            score += points;

            // 3. CHECK IF WE BEAT THE RECORD LIVE!
            if (score > highScore)
            {
                highScore = score; // Update the internal number
                PlayerPrefs.SetInt("BestScore", highScore); // Save it permanently!
                PlayerPrefs.Save(); 
                
                // Update the gold text on the screen
                if (highScoreText != null) 
                {
                    highScoreText.text = "HIGH SCORE: " + highScore;
                }
            }
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        if (gameOverPanel != null) gameOverPanel.SetActive(true); 

        // --- NEW AUTO-REBOOT LOGIC ---
        // Tell Unity to trigger the 'RestartGame' function exactly 10 seconds from now
        Invoke("RestartGame", 10f);
    }

    public void RestartGame()
    {
        // This will now happen automatically!
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}