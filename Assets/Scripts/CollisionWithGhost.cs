using UnityEngine;

public class CollisionWithGhost : MonoBehaviour
{
    [SerializeField] private AudioClip ghostCollisionSound;

    private GameManager gameManager;
    private AudioSource audioSource;

    void Start()
    {
        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            audioSource?.PlayOneShot(ghostCollisionSound);
            Destroy(other.gameObject);
            gameManager?.UpdateLives(-1);
        }
    }
}