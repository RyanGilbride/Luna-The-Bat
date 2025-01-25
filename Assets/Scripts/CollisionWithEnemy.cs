using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public AudioClip collisionSound; // Public audio clip for collision
    private GameManager gameManager;
    private AudioSource audioSource; // AudioSource component

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>(); // Assign the AudioSource component
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Destroy the enemy object
            Destroy(other.gameObject);

            // Update lives
            gameManager.UpdateLives(1);

            // Play the collision sound
            if (collisionSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(collisionSound, 1.0f);
            }
        }
    }
}
