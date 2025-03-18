using UnityEngine;

public class GhostMove : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float bounceSpeed = 1.0f;
    [SerializeField] private float minY = -4.0f;
    [SerializeField] private float maxY = 10.0f;

    private Vector3 initialPosition;
    private GameManager gameManager;
    private float lastLoggedSpeed = -1f; // Track the last logged speed

    void Start()
    {
        initialPosition = transform.position;
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
                string logMessage = $"Adjusted Ghost Speed: {adjustedSpeed:F2}";
                Debug.Log(logMessage);
                gameManager?.LogData(logMessage);
                lastLoggedSpeed = adjustedSpeed; // Update the last logged speed
            }

            // Move the ghost
            transform.Translate(Vector3.forward * Time.deltaTime * adjustedSpeed);

            // Bounce effect
            float newY = Mathf.Lerp(minY, maxY, Mathf.PingPong(Time.time * bounceSpeed, 1));
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}