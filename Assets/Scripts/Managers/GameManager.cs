using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    void Start()
    {
        titleScreen.SetActive(true);
        lives = 3;
        livesText.text = "Lives: " + lives;
        totalScore = 0;
        scoreText.text = "Score: " + totalScore;
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
            GameOver(); // Call the GameOver method
        }

        // Track misses for adaptive difficulty
        consecutiveMisses++;
        consecutiveHits = 0;
    }

    public void UpdateScore(int scoreToAdd)
    {
        totalScore += scoreToAdd;
        scoreText.text = "Score: " + totalScore;

        // Track hits for adaptive difficulty
        consecutiveHits++;
        consecutiveMisses = 0;
    }

    private void AdjustDifficulty()
    {
        // Increase difficulty if player is doing well
        if (consecutiveHits >= 5)
        {
            difficultyMultiplier += 0.1f; // Increase difficulty
            consecutiveHits = 0; // Reset counter
        }

        // Decrease difficulty if player is struggling
        if (consecutiveMisses >= 3)
        {
            difficultyMultiplier = Mathf.Max(1.0f, difficultyMultiplier - 0.1f); // Decrease difficulty (minimum 1.0)
            consecutiveMisses = 0; // Reset counter
        }

        // Broadcast difficulty to other scripts
        BroadcastMessage("OnDifficultyChanged", difficultyMultiplier, SendMessageOptions.DontRequireReceiver);
      
        if (consecutiveHits >= 5)
        {
            difficultyMultiplier += 0.1f;
            Debug.Log($"Difficulty Increased: {difficultyMultiplier}");
            consecutiveHits = 0;
        }

        if (consecutiveMisses >= 3)
        {
            difficultyMultiplier = Mathf.Max(1.0f, difficultyMultiplier - 0.1f);
            Debug.Log($"Difficulty Decreased: {difficultyMultiplier}");
            consecutiveMisses = 0;
        }
    }


    // GameOver Method
    public void GameOver()
    {
        isGameActive = false;
        gameOverScreen.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    // Other methods remain unchanged...
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        titleScreen.SetActive(false);
        totalScore = 0;
        UpdateScore(0);
        UpdateLives(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelOne()
    {
        SceneManager.LoadScene("LunaTheBat1");
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene("LunaTheBat2");
    }

    public void LevelThree()
    {
        SceneManager.LoadScene("LunaTheBat3");
    }

    public void LevelFour()
    {
        SceneManager.LoadScene("LunaTheBat4");
    }

    public void ChangePaused()
    {
        paused = !paused;
        pauseScreen.SetActive(paused);
        Time.timeScale = paused ? 0 : 1;
    }

    public void HelpScreen()
    {
        helpScreen.SetActive(true);
    }

    public float GetDifficultyMultiplier()
    {
        return difficultyMultiplier;
    }

}