using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject powerupPrefab;
    private Vector3 spawnPosObs = new Vector3(25, 0, 0);
    private Vector3 spawnPosPU = new Vector3(50, 0, 0);
    private float startDelay = 2.0f;
    public float repeatRate;
    private PlayerController playerControllerScript;
    private GameManager gameManager;

    // Adaptive Difficulty Variables
    private float initialRepeatRate;

    void Start()
    {
        initialRepeatRate = repeatRate; // Store initial repeat rate
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        InvokeRepeating("SpawnPowerUp", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false && gameManager.isGameActive == true)
        {
            Instantiate(obstaclePrefab, spawnPosObs, obstaclePrefab.transform.rotation);
        }
    }

    void SpawnPowerUp()
    {
        if (playerControllerScript.gameOver == false && gameManager.isGameActive == true)
        {
            Instantiate(powerupPrefab, spawnPosPU, powerupPrefab.transform.rotation);
        }
    }

    // Adjust spawn rate based on difficulty
    private void OnDifficultyChanged(float difficultyMultiplier)
    {
        repeatRate = initialRepeatRate / difficultyMultiplier; // Faster spawns as difficulty increases
        CancelInvoke("SpawnObstacle");
        CancelInvoke("SpawnPowerUp");
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        InvokeRepeating("SpawnPowerUp", startDelay, repeatRate);
    }
}