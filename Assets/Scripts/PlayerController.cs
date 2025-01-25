using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce = 15.0f; // Default jump force value
    public float gravityModifier = 2.0f; // Default gravity modifier
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool hasPowerUp;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip shootSound; // Shooting sound
    public AudioClip gameOverSound; // Game over sound
    public AudioClip powerupConsume;
    public AudioSource playerAudio;
    public GameObject projectilePrefab;
    public GameManager gameManager;
    public TextMeshProUGUI gameOverText;

    private ProjectilePool projectilePool;
    private bool canShoot = true; // Tracks if the player can shoot
    [SerializeField] private float shootCooldown = 1.5f; // Cooldown time for shooting

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier; // Allows player to adjust the gravity 
        playerAudio = GetComponent<AudioSource>(); // Enables audio to play 
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        projectilePool = GameObject.Find("ProjectilePool").GetComponent<ProjectilePool>(); // Get the projectile pool
    }

    // Update is called once per frame
    void Update()
    {
        // Handle projectile launching
        if (Input.GetKeyDown(KeyCode.Tab) && canShoot && !gameOver)
        {
            LaunchProjectile();
        }

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * 15, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f); // Plays jump sound when player presses space bar 
        }
    }
    private void LaunchProjectile()
    {
        GameObject projectile = projectilePool.GetProjectile(); // Get a projectile from the pool
        projectile.transform.position = transform.position; // Set its position to the player's position
        projectile.transform.rotation = projectilePrefab.transform.rotation; // Set its rotation

        // Play shooting sound
        if (playerAudio != null && shootSound != null)
        {
            playerAudio.PlayOneShot(shootSound, 1.0f);
        }

        StartCoroutine(ShootCooldownRoutine());
    }

    private IEnumerator ShootCooldownRoutine()
    {
        canShoot = false; // Disable shooting
        yield return new WaitForSeconds(shootCooldown); // Wait for cooldown duration
        canShoot = true; // Re-enable shooting
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true; // Prevents player double jumping
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle") && !gameOver)
        {
            HandleGameOver(); // Trigger Game Over logic
        }
    }

    private void HandleGameOver()
    {
        Debug.Log("Game Over!");
        gameOver = true;

        // Notify the game manager
        gameManager.GameOver();

        // Stop other sounds and play the game over sound
        playerAudio.Stop(); // Stop any currently playing audio
        if (playerAudio != null && gameOverSound != null)
        {
            playerAudio.PlayOneShot(gameOverSound, 1.0f);
        }

        // Trigger game over animations
        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", 1);

        // Play explosion effect
        explosionParticle.Play();
        dirtParticle.Stop();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            gameManager.UpdateScore(5); // Score is increased by 5
            hasPowerUp = true;
            Destroy(other.gameObject); // Remove the power-up
            playerAudio.PlayOneShot(powerupConsume, 1.0f); // Play power-up sound
        }
    }
}
