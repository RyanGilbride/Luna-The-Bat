using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePowerUpLeft : MonoBehaviour
{
    public float speed = 10;
    private PlayerController playerControllerScript;
    private float leftBound = -15;
    private GameManager gameManager;
    private float bounceSpeed = 1.0f; // Speed of the bounce
    private float bounceHeight =5.0f; // Height of the bounce
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript =
            GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        initialPosition = transform.position; // Store the starting position of the object
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false && gameManager.isGameActive == true)
        {
            // Move the object to the left
            transform.Translate(Vector3.back * Time.deltaTime * speed);

            // Bounce the object between y = 0 and y = 1
            float newY = initialPosition.y + Mathf.PingPong(Time.time * bounceSpeed, bounceHeight);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        // Remove obstacles when they go off-screen
        if (transform.position.x < leftBound && gameObject.CompareTag("PowerUp"))
        {
            Destroy(gameObject);
        }
    }
}
