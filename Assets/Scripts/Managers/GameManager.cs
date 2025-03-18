using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO; // Required for file operations

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button levelButton;
    [SerializeField] private Button helpButton;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject goalScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject helpScreen;
    [SerializeField] private float goalScore = 100.0f;

    public static int totalScore;
    public static int lives;
    public bool isGameActive;
    private bool paused;

    // Adaptive Difficulty Variables
    private float difficultyMultiplier = 1.0f; // Scales difficulty based on player performance
    private int consecutiveHits = 0; // Tracks successful actions (e.g., kills, dodges)
    private int consecutiveMisses = 0; // Tracks failures (e.g., hits taken)

    // Data Logging Variables
    private string logFilePath;

    void Start()
    {
        titleScreen.SetActive(true);
        lives = 3;
        livesText.text = "Lives: " + lives;
        totalScore = 0;
        scoreText.text = "Score: " + totalScore;

        // Initialize data logging
        logFilePath = Application.dataPath + "/gameLog.txt";
        LogData("Game Started");
    }

    void Update()
    {
        if (totalScore >= goalScore)
        {
            isGameActive = false;
            goalScreen.SetActive(true);
            levelButton.gameObject.SetActive(true);
        }

        // Adjust difficulty dynamically
        AdjustDifficulty();
    }

    public void UpdateLives(int livesToRemove)
    {
        lives -= livesToRemove;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            GameOver();
        }

        // Track misses for adaptive difficulty
        consecutiveMisses++;
        consecutiveHits = 0;

        // Log lives update
        Debug.Log($"Lives Updated: {lives}");
        LogData($"Lives Updated: {lives}");
    }

    public void UpdateScore(int scoreToAdd)
    {
        totalScore += scoreToAdd;
        scoreText.text = "Score: " + totalScore;

        // Track hits for adaptive difficulty
        consecutiveHits++;
        consecutiveMisses = 0;

        // Log score update
        Debug.Log($"Score Updated: {totalScore}");
        LogData($"Score Updated: {totalScore}");
    }

    private void AdjustDifficulty()
    {
        // Increase difficulty if player is doing well
        if (consecutiveHits >= 5)
        {
            difficultyMultiplier += 0.1f; // Increase difficulty
            consecutiveHits = 0; // Reset counter

            // Log difficulty increase
            Debug.Log($"Difficulty Increased: {difficultyMultiplier}");
            LogData($"Difficulty Increased: {difficultyMultiplier}");
        }

        // Decrease difficulty if player is struggling
        if (consecutiveMisses >= 3)
        {
            difficultyMultiplier = Mathf.Max(1.0f, difficultyMultiplier - 0.1f); // Decrease difficulty (minimum 1.0)
            consecutiveMisses = 0; // Reset counter

            // Log difficulty decrease
            Debug.Log($"Difficulty Decreased: {difficultyMultiplier}");
            LogData($"Difficulty Decreased: {difficultyMultiplier}");
        }

        // Broadcast difficulty to other scripts
        BroadcastMessage("OnDifficultyChanged", difficultyMultiplier, SendMessageOptions.DontRequireReceiver);
    }

    // GameOver Method
    public void GameOver()
    {
        isGameActive = false;
        gameOverScreen.SetActive(true);
        restartButton.gameObject.SetActive(true);

        // Log game over
        Debug.Log("Game Over!");
        LogData("Game Over!");
    }

    // Data Logging Method
    public void LogData(string message)

    {
        string logEntry = $"[{System.DateTime.Now}] {message}";
        File.AppendAllText(logFilePath, logEntry + "\n");
    }

    // Method to get the current difficulty multiplier
    public float GetDifficultyMultiplier()
    {
        return difficultyMultiplier;
    }

    // Other methods remain unchanged...
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        titleScreen.SetActive(false);
        totalScore = 0;
        UpdateScore(0);
        UpdateLives(0);

        // Log game start
        Debug.Log("Game Started");
        LogData("Game Started");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Log game restart
        Debug.Log("Game Restarted");
        LogData("Game Restarted");
    }

    public void LevelOne()
    {
        SceneManager.LoadScene("LunaTheBat1");

        // Log level change
        Debug.Log("Level 1 Loaded");
        LogData("Level 1 Loaded");
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene("LunaTheBat2");

        // Log level change
        Debug.Log("Level 2 Loaded");
        LogData("Level 2 Loaded");
    }

    public void LevelThree()
    {
        SceneManager.LoadScene("LunaTheBat3");

        // Log level change
        Debug.Log("Level 3 Loaded");
        LogData("Level 3 Loaded");
    }

    public void LevelFour()
    {
        SceneManager.LoadScene("LunaTheBat4");

        // Log level change
        Debug.Log("Level 4 Loaded");
        LogData("Level 4 Loaded");
    }

    public void ChangePaused()
    {
        paused = !paused;
        pauseScreen.SetActive(paused);
        Time.timeScale = paused ? 0 : 1;

        // Log pause state
        Debug.Log($"Game Paused: {paused}");
        LogData($"Game Paused: {paused}");
    }

    public void HelpScreen()
    {
        helpScreen.SetActive(true);

        // Log help screen opened
        Debug.Log("Help Screen Opened");
        LogData("Help Screen Opened");
    }
}