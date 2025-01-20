using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerUpObtained : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject prefabToDestroy; //assign the prefab to destroy, in this case it's the powerup
    public AudioSource playerAudio;
    public AudioClip powerupConsume;



    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other) //destroys PowerUp / Pumpkin when collided with Player.
    {
        if (other.CompareTag("PowerUp"))
        {
            gameManager.UpdateScore(5); //score is increased by 5 and game object is destroyed  
            Destroy(other.gameObject); //makes powerup disappear
            playerAudio.PlayOneShot(powerupConsume, 1.0f); //eating sound plays when player collides with powerup

        }
    }

}