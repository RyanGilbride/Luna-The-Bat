using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithGhost : MonoBehaviour
{
    public GameObject ghostPrefab; // Reference to the ghost prefab
    private GameManager gameManager; // Reference to the GameManager
    public AudioClip ghostCollisionSound; // Audio clip to play upon collision
    private AudioSource audioSource; // Audio source component

    // Start is called before the first frame update
    void Start()
    {
        // Find the GameManager in the scene
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Get or add an AudioSource component
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Triggered when another collider enters this object's trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the tag "Ghost"
        if (other.CompareTag("Ghost"))
        {
            // Play the collision sound if assigned
            if (ghostCollisionSound != null)
            {
                audioSource.PlayOneShot(ghostCollisionSound);
            }

            // Destroy the ghost upon collision
            Destroy(other.gameObject);

            // Add 1 life to the player
            gameManager.UpdateLives(-1);
        }
    }
}
