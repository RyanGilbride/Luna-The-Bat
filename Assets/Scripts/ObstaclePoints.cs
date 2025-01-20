using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePoints : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name == "WoodenCrate")
        {
            Debug.Log("Hit Obstacle");
        }
    }
}
