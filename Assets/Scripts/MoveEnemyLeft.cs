using UnityEngine;

public class MoveEnemyLeft : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;

    private GameManager gameManager;
    private float lastLoggedSpeed = -1f; // Track the last logged speed

    void Start()
    {
        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager != null && gameManager.isGameActive)
        {
            // Adjust speed based on difficulty
            float adjustedSpeed = speed * gameManager.GetDifficultyMultiplier();

            // Log the adjusted speed ONLY if it changes significantly
            if (!Mathf.Approximately(adjustedSpeed, lastLoggedSpeed))
            {
                string logMessage = $"Adjusted Enemy Speed: {adjustedSpeed:F2}";
                Debug.Log(logMessage);
                gameManager?.LogData(logMessage);
                lastLoggedSpeed = adjustedSpeed; // Update the last logged speed
            }

            // Move the enemy
            transform.Translate(Vector3.forward * Time.deltaTime * adjustedSpeed);
        }
    }
}