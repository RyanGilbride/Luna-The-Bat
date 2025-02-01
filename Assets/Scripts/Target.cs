using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Deactivate the projectile and destroy the target
        other.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}