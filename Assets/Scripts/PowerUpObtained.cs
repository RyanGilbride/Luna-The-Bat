using UnityEngine;
using TMPro; // For UI feedback

public class PowerUpObtained : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioClip powerupConsume;

    // For UI feedback
    [SerializeField] private TextMeshProUGUI powerupEffectText; // Assign in Inspector

    private AudioSource playerAudio;

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            // Adjust score based on difficulty
            int scoreIncrease = (int)(5 * gameManager.GetDifficultyMultiplier());
            gameManager?.UpdateScore(scoreIncrease);

            // Log the power-up effect to the console and gameLog.txt
            string logMessage = $"Power-Up Collected! Score Increased by: {scoreIncrease}";
            Debug.Log(logMessage);
            gameManager?.LogData(logMessage);

            Destroy(other.gameObject);
            playerAudio.PlayOneShot(powerupConsume, 1.0f);
        }
    }
}