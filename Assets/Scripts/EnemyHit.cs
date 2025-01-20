using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class EnemyHit : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    
    public GameObject enemyPrefab;
    public AudioSource playerAudio;
    public AudioClip enemyKilled;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other) //destroys obstacles when collided with projectile.
    {
        if (other.CompareTag("Enemy"))
        {
            gameManager.UpdateScore(10);//score is increased by 10 and game object is destroyed  
            Destroy(other.gameObject); //destroys projectile
            Destroy(gameObject);
            //Destroy(enemyPrefab);//destroys enemy when hit
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
