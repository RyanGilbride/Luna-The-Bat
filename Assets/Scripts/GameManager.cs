using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //variables
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI timeText;
    public static int totalScore;
    public static int lives;
    public List <GameObject> targets;
    public Button restartButton;
    public Button levelButton;
    public Button helpButton;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public GameObject goalScreen;
    public GameObject pauseScreen;
    public GameObject helpScreen;
    public Scene LunaTheBat1;
    public Scene LunaTheBat2;
    public Scene LunaTheBat3;
    public Scene LunaTheBat4;
    public EnemySpawnManager enemySpawnManager;
    public MoveEnemyLeft moveEnemyLeft;
    public MoveLeft moveLeft;
    public bool isGameActive;
    private bool paused;
    public float goalScore;
    public bool goalScore1;
    public bool goalScore2;
    public bool goalScore3;

    // Start is called before the first frame update
    void Start()
    {
        titleScreen.gameObject.SetActive(true);
        lives = 3;
        livesText.text = "Lives: " + lives;

        totalScore = 0;
        scoreText.text = "Score: " + totalScore;
        goalScore1= false;
        goalScore2= false;
        goalScore3= false;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
        if (totalScore >= goalScore)
        {
            goalScore1 = true;
            isGameActive = false;

            goalScreen.gameObject.SetActive(true);
            levelButton.gameObject.SetActive(true);
        }

    }
    public void UpdateLives(int livesToRemove)
    {
        //managing lives
        lives -= livesToRemove;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            GameOver();


        }
        Debug.Log("Lives = " + lives);


    }

    public void UpdateScore(int scoreToAdd)
    {
        //score manager
        totalScore += scoreToAdd;
        scoreText.text = "Score: " + totalScore;
        
        

    }

    public void StartGame(int difficulty)
    {
        //will run when start button is pressed
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
        enemySpawnManager.SpawnEnemy();
        totalScore = 0;
        UpdateScore(0);
        UpdateLives(0);

    }

    public void RestartGame()
    {
        // will run when restart button is pressed
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        //activates when player collides with obstacle 
        isGameActive = false;
        gameOverScreen.gameObject.SetActive(true);
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


    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void HelpScreen()
    {
        helpScreen.gameObject.SetActive(true);

    }
}