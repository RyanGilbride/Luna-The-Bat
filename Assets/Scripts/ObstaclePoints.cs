using UnityEngine;

public class ObstaclePoints : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "WoodenCrate")
        {
            Debug.Log("Hit Obstacle");
        }
    }
}