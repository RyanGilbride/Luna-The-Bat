using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // Instead of destroying the projectile when it collides with an animal
        //Destroy(other.gameObject); 

        // Just deactivate the food and destroy the animal
        other.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
