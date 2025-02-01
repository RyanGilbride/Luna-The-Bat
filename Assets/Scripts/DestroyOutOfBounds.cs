using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    [SerializeField] private float rightBound = 30.0f;

    void Update()
    {
        if (transform.position.x > rightBound)
        {
            Destroy(gameObject);
        }
    }
}