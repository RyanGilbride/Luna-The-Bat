using UnityEngine;
using TMPro;

public class MovePowerUpLeft : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float bounceSpeed = 1.0f;
    [SerializeField] private float bounceHeight = 5.0f;

    // For UI feedback
    [SerializeField] private TextMeshProUGUI powerupSpeedText; // Assign in Inspector

    private Vector3 initialPosition;
    private PlayerController playerControllerScript;
    private GameManager gameManager;
    private float lastLoggedSpeed = -1f; // Track the last logged speed

    void Start()
    {
        initialPosition = transform.position;
        playerControllerScript = GameObject.Find("Player")?.GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();

    }

    void Update()
    {
        if (playerControllerScript != null && !playerControllerScript.gameOver && gameManager != null && gameManager.isGameActive)
        {
            // Adjust speed based on difficulty
            float adjustedSpeed = speed * gameManager.GetDifficultyMultiplier();

            // Log the adjusted speed ONLY if it changes significantly
            if (!Mathf.Approximately(adjustedSpeed, lastLoggedSpeed))
            {
                string logMessage = $"Adjusted Power-Up Speed: {adjustedSpeed:F2}";
                Debug.Log(logMessage);
                gameManager?.LogData(logMessage);
                lastLoggedSpeed = adjustedSpeed; // Update the last logged speed
            }

            // Move the power-up
            transform.Translate(Vector3.back * Time.deltaTime * adjustedSpeed);

            // Bounce effect
            float newY = initialPosition.y + Mathf.PingPong(Time.time * bounceSpeed, bounceHeight);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        if (transform.position.x < -15 && gameObject.CompareTag("PowerUp"))
        {
            Destroy(gameObject);
        }
    }
}