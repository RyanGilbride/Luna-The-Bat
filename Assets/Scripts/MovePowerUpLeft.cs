using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePowerUpLeft : MonoBehaviour
{
    public float speed = 10;
    private PlayerController playerControllerScript;
    private float leftBound = -15;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript =
            GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false && gameManager.isGameActive == true)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        //remove obstacles when the go off screen
        if (transform.position.x < leftBound && gameObject.CompareTag("PowerUp"))
        {
            Destroy(gameObject);
        }
    }
}
