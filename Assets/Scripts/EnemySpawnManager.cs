using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private PlayerController playerControllerScript;
    private EnemySpawnManager spawnEnemyManagerScript;
    private MoveEnemyLeft moveEnemyLeft;

    private Vector3 spawnPosEnemy = new Vector3(30, 0, 0);
    private float startDelay = 2.0f;
    public float repeatRate;


    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("SpawnEnemy", startDelay, repeatRate); //loops spawning of enemys with first spawning after 2 secs and then repeating every 20 secs
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();



    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPosEnemy, enemyPrefab.transform.rotation); //spawns skeletons
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

    }


}