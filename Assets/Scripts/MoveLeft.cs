using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;

    private PlayerController playerControllerScript;
    private GameManager gameManager;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player")?.GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();
    }

    void Update()
    {
        if (playerControllerScript != null && !playerControllerScript.gameOver && gameManager != null && gameManager.isGameActive)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.x < -15 && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}