using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyLeft : MonoBehaviour
{
    public float speed;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

        }
    }
}
