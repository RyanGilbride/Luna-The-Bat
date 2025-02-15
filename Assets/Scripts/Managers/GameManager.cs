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
        // Removed the "P" key input check here

        if (totalScore >= goalScore)
        {
            isGameActive = false;
            goalScreen.SetActive(true);
            levelButton.gameObject.SetActive(true);
        }
    }

    public void UpdateLives(int livesToRemove)
    {
        lives -= livesToRemove;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        totalScore += scoreToAdd;
        scoreText.text = "Score: " + totalScore;
    }

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

    public void GameOver()
    {
        isGameActive = false;
        gameOverScreen.SetActive(true);
        restartButton.gameObject.SetActive(true);
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

    // Made this method public to call from a UI button
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
}