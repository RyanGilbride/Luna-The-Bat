using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithGhost : MonoBehaviour
{
    public GameObject ghostPrefab; // Reference to the ghost prefab
    private GameManager gameManager; // Reference to the GameManager

    // Start is called before the first frame update
    void Start()
    {
        // Find the GameManager in the scene
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Triggered when another collider enters this object's trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the tag "Ghost"
        if (other.CompareTag("Ghost"))
        {
            //Destroy(other.gameObject); // Destroy the ghost upon collision

            gameManager.UpdateLives(-1); // Add 1 life to the player
        }
    }
}
