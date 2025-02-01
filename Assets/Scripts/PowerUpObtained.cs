using UnityEngine;

public class PowerUpObtained : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioClip powerupConsume;

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
            gameManager?.UpdateScore(5);
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(powerupConsume, 1.0f);
        }
    }
}