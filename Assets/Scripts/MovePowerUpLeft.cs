using UnityEngine;

public class MovePowerUpLeft : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float bounceSpeed = 1.0f;
    [SerializeField] private float bounceHeight = 5.0f;

    private Vector3 initialPosition;
    private PlayerController playerControllerScript;
    private GameManager gameManager;

    void Start()
    {
        initialPosition = transform.position;
        playerControllerScript = GameObject.Find("Player")?.GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();
    }

    void Update()
    {
        if (playerControllerScript != null && !playerControllerScript.gameOver && gameManager != null && gameManager.isGameActive)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
            float newY = initialPosition.y + Mathf.PingPong(Time.time * bounceSpeed, bounceHeight);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        if (transform.position.x < -15 && gameObject.CompareTag("PowerUp"))
        {
            Destroy(gameObject);
        }
    }
}