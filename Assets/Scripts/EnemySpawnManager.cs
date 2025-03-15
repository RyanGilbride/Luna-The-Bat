using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private Vector3 spawnPosEnemy = new Vector3(30, 0, 0);
    [SerializeField] private Vector3 spawnPosGhost = new Vector3(30, 0, 0);
    [SerializeField] private float startDelay = 2.0f;
    [SerializeField] private float repeatRate = 2.0f;

    private PlayerController playerControllerScript;
    private GameManager gameManager;

    // Dynamic AI Variables
    private float initialRepeatRate;

    void Start()
    {
        initialRepeatRate = repeatRate; // Store initial repeat rate
        playerControllerScript = GameObject.Find("Player")?.GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();

        if (playerControllerScript == null || gameManager == null)
        {
            Debug.LogError("PlayerController or GameManager not found!");
            return;
        }

        InvokeRepeating("SpawnEnemy", startDelay, repeatRate);
        InvokeRepeating("SpawnGhost", startDelay + 1.0f, repeatRate + 1.0f);
    }

    public void SpawnEnemy()
    {
        if (!playerControllerScript.gameOver)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPosEnemy, enemyPrefab.transform.rotation);
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.SetDifficulty(gameManager.GetDifficultyMultiplier()); // Pass difficulty to AI
            }
        }
    }

    public void SpawnGhost()
    {
        if (!playerControllerScript.gameOver)
        {
            GameObject ghost = Instantiate(ghostPrefab, spawnPosGhost, ghostPrefab.transform.rotation);
            EnemyAI ghostAI = ghost.GetComponent<EnemyAI>();
            if (ghostAI != null)
            {
                ghostAI.SetDifficulty(gameManager.GetDifficultyMultiplier()); // Pass difficulty to AI
            }
        }
    }

    // Adjust spawn rate and AI behavior based on difficulty
    private void OnDifficultyChanged(float difficultyMultiplier)
    {
        repeatRate = initialRepeatRate / difficultyMultiplier; // Faster spawns as difficulty increases
        CancelInvoke("SpawnEnemy");
        CancelInvoke("SpawnGhost");
        InvokeRepeating("SpawnEnemy", startDelay, repeatRate);
        InvokeRepeating("SpawnGhost", startDelay + 1.0f, repeatRate + 1.0f);
    }
}