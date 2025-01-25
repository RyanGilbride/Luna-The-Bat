using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile prefab
    public int poolSize = 10; // Number of projectiles in the pool
    private Queue<GameObject> projectilePool = new Queue<GameObject>(); // Queue to hold the projectiles

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the pool with projectiles
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false); // Deactivate the projectile initially
            projectilePool.Enqueue(projectile); // Add it to the pool
        }
    }

    // Method to get a projectile from the pool
    public GameObject GetProjectile()
    {
        if (projectilePool.Count > 0)
        {
            GameObject projectile = projectilePool.Dequeue(); // Get a projectile from the pool
            projectile.SetActive(true); // Activate the projectile
            return projectile;
        }
        else
        {
            // If the pool is empty, create a new projectile
            GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.SetActive(true);
            return newProjectile;
        }
    }

    // Method to return a projectile to the pool
    public void ReturnProjectileToPool(GameObject projectile)
    {
        projectile.SetActive(false); // Deactivate the projectile
        projectilePool.Enqueue(projectile); // Add it back to the pool
    }
}
