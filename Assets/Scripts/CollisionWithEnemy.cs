using UnityEngine;

public class CollisionWithEnemy : MonoBehaviour
{
    [SerializeField] private AudioClip collisionSound;

    private GameManager gameManager;
    private AudioSource audioSource;

    void Start()
    {
        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            gameManager?.UpdateLives(1);
            audioSource?.PlayOneShot(collisionSound, 1.0f);
        }
    }
}