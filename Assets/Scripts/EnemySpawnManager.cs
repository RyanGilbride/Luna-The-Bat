using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private Vector3 spawnPosEnemy = new Vector3(30, 0, 0);
    [SerializeField] private Vector3 spawnPosGhost = new Vector3(30, 0, 0);
    [SerializeField] private float startDelay = 2.0f;
    [SerializeField] private float repeatRate = 2.0f;

    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player")?.GetComponent<PlayerController>();

        if (playerControllerScript == null)
        {
            Debug.LogError("PlayerController not found!");
            return;
        }

        InvokeRepeating("SpawnEnemy", startDelay, repeatRate);
        InvokeRepeating("SpawnGhost", startDelay + 1.0f, repeatRate + 1.0f);
    }

    public void SpawnEnemy()
    {
        if (!playerControllerScript.gameOver)
        {
            Instantiate(enemyPrefab, spawnPosEnemy, enemyPrefab.transform.rotation);
        }
    }

    public void SpawnGhost()
    {
        if (!playerControllerScript.gameOver)
        {
            Instantiate(ghostPrefab, spawnPosGhost, ghostPrefab.transform.rotation);
        }
    }
}