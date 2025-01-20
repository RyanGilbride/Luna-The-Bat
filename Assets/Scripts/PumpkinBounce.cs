using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinBounce : MonoBehaviour
{
    [SerializeField] private float bounceSpeed = 1.0f; // Speed of the bounce
    [SerializeField] private float bounceHeight = 1.0f; // Height of the bounce

    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Store the starting position of the pumpkin
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new Y position using a sine wave
        float newY = initialPosition.y + Mathf.PingPong(Time.time * bounceSpeed, bounceHeight);

        // Update the object's position
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}
