using UnityEngine;
using TMPro;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    private float scrollSpeed = 5.0f; // Base scroll speed
    private GameManager gameManager;
    private float lastLoggedSpeed = -1f; // Track the last logged speed

    // For UI feedback
    [SerializeField] private TextMeshProUGUI scrollSpeedText; // Assign in Inspector

    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();

    }

    void Update()
    {
        if (gameManager != null && gameManager.isGameActive)
        {
            // Adjust scroll speed based on difficulty
            float adjustedSpeed = scrollSpeed * gameManager.GetDifficultyMultiplier();

            // Log to file ONLY if the speed changes significantly
            if (!Mathf.Approximately(adjustedSpeed, lastLoggedSpeed))
            {
                string logMessage = $"Adjusted Scroll Speed: {adjustedSpeed:F2}";
                Debug.Log(logMessage);
                gameManager.LogData(logMessage);
                lastLoggedSpeed = adjustedSpeed; // Update the last logged speed
            }


            // Move the background
            transform.Translate(Vector3.left * adjustedSpeed * Time.deltaTime);

            // Loop the background
            if (transform.position.x < startPos.x - repeatWidth)
            {
                transform.position = startPos;
            }
        }
    }
}