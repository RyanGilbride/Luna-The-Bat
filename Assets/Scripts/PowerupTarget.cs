using UnityEngine;

public class PowerupTarget : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject prefabtoDestroy;
    [SerializeField] private AudioClip powerupConsume;

    private AudioSource playerAudio;

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Destroy(prefabtoDestroy);
            playerAudio.PlayOneShot(powerupConsume, 1.0f);
            gameManager?.UpdateScore(5);
        }
    }
}