using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
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

    void OnTriggerEnter(Collider other) //destroys obstacles when collided with projectile.
    {
        if (other.CompareTag("Obstacle"))
        {

            Destroy(gameObject); //destroys projectile
            Destroy(other.gameObject);//destroys box
            gameManager.UpdateScore(1); //score is increased by 1 and game object is destroyed  
        }
        
    }



}