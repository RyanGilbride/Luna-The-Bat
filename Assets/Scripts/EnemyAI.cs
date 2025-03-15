using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float speed = 5.0f; // Base speed
    private float attackCooldown = 2.0f; // Base attack cooldown

    public void SetDifficulty(float difficultyMultiplier)
    {
        speed *= difficultyMultiplier;
        attackCooldown /= difficultyMultiplier;
        Debug.Log($"Enemy Speed: {speed}, Attack Cooldown: {attackCooldown}");
        ApplyDifficultyChanges();
    }

    private void ApplyDifficultyChanges()
    {
        // Adjust movement speed
        if (TryGetComponent(out Rigidbody rb))
        {
            rb.velocity = new Vector3(-speed, 0, 0); // Move left at adjusted speed
        }

    }
}
