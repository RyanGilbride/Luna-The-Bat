using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    public float speed;
    private GameManager gameManager;
    private float bounceSpeed = 1.0f; // Speed of the vertical movement
    private float minY = -4.0f; // Minimum Y position
    private float maxY = 10.0f; // Maximum Y position
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        initialPosition = transform.position; // Store the initial position of the enemy
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive == true)
        {
            // Move the enemy forward
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            // Calculate the new Y position for the smooth up-and-down motion
            float newY = Mathf.Lerp(minY, maxY, Mathf.PingPong(Time.time * bounceSpeed, 1));
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
