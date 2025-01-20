using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Original enemy prefab
    public GameObject ghostPrefab; // New enemy prefab
    private PlayerController playerControllerScript;

    private Vector3 spawnPosEnemy = new Vector3(30, 0, 0); // Spawn position for the original enemy
    private Vector3 spawnPosGhost = new Vector3(30, 0, 0); // Spawn position for the ghost
    private float startDelay = 2.0f; // Delay before spawning begins
    public float repeatRate; // Interval between spawns

    // Start is called before the first frame update
    void Start()
    {
        // Loop the spawning of enemies
        InvokeRepeating("SpawnEnemy", startDelay, repeatRate);
        InvokeRepeating("SpawnGhost", startDelay + 1.0f, repeatRate + 1.0f); // Slightly offset to alternate spawns

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Spawn the original enemy
    public void SpawnEnemy()
    {
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(enemyPrefab, spawnPosEnemy, enemyPrefab.transform.rotation);
        }
    }

    // Spawn the ghost enemy
    public void SpawnGhost()
    {
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(ghostPrefab, spawnPosGhost, ghostPrefab.transform.rotation);
        }
    }
}
