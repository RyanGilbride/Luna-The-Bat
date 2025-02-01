using UnityEngine;

public class GhostMove : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float bounceSpeed = 1.0f;
    [SerializeField] private float minY = -4.0f;
    [SerializeField] private float maxY = 10.0f;

    private Vector3 initialPosition;
    private GameManager gameManager;

    void Start()
    {
        initialPosition = transform.position;
        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager != null && gameManager.isGameActive)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            float newY = Mathf.Lerp(minY, maxY, Mathf.PingPong(Time.time * bounceSpeed, 1));
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}