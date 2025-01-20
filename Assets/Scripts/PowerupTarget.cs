using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTarget : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject prefabtoDestroy;
    public AudioSource playerAudio;
    public AudioClip powerupConsume;
    public int pointValue;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(prefabtoDestroy); //makes powerup disappear
        playerAudio.PlayOneShot(powerupConsume, 1.0f); //eating sound plays when player collides with powerup
        gameManager.UpdateScore(5);//score is increased by 5 and game object is destroyed  

    }


    /*void OnTriggerEnter(Collider other) //destroys obstacles when collided with projectile.
    {
        if (other.CompareTag("PowerUp"))
        {
        
            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            gameManager.UpdateScore(5);//score is increased by 5 and game object is destroyed  
            Destroy(other.gameObject); //makes powerup disappear
            playerAudio.PlayOneShot(powerupConsume, 1.0f); //eating sound plays when player collides with powerup
        }
    }*/

}