using UnityEngine;

public class PumpkinBounce : MonoBehaviour
{
    [SerializeField] private float bounceSpeed = 1.0f;
    [SerializeField] private float bounceHeight = 1.0f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float newY = initialPosition.y + Mathf.PingPong(Time.time * bounceSpeed, bounceHeight);
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}