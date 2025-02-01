using UnityEngine;

public class MoveEnemyLeft : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager != null && gameManager.isGameActive)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }
}