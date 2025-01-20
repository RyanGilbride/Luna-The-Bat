using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithEnemy : MonoBehaviour
{

    public GameObject enemyPrefab;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Enemy"))
        {

            Destroy(other.gameObject); //destroys enemy upon collision with player
            
            gameManager.UpdateLives(1); //decreases lives by 1 upon collision with enemy prefab
        }
    }
}
