using UnityEngine;
using TMPro; // For UI feedback

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    private float scrollSpeed = 5.0f; // Base scroll speed
    private GameManager gameManager;

    // For UI feedback
    [SerializeField] private TextMeshProUGUI scrollSpeedText; // Assign in Inspector

    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2; // Use half the width for seamless looping
        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();

    }

    void Update()
    {
        if (gameManager != null && gameManager.isGameActive)
        {
            // Adjust scroll speed based on difficulty
            float adjustedSpeed = scrollSpeed * gameManager.GetDifficultyMultiplier();

            // Log the adjusted scroll speed to the console
            //Debug.Log($"Adjusted Scroll Speed: {adjustedSpeed:F2}");

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