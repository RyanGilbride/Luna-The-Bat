using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool hasPowerUp;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip powerupConsume;
    public AudioSource playerAudio;
    public GameObject projectilePrefab;
    public GameManager gameManager;
    public TextMeshProUGUI gameOverText;

    private bool canShoot = true; // Tracks if the player can shoot
    [SerializeField] private float shootCooldown = 1.5f; // Cooldown time set to 1.5 seconds

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier; // Allows player to adjust the gravity 
        playerAudio = GetComponent<AudioSource>(); // Enables audio to play 
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle projectile launching
        if (Input.GetKeyDown(KeyCode.Tab) && canShoot)
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
        Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        StartCoroutine(ShootCooldownRoutine());
    }

    private IEnumerator ShootCooldownRoutine()
    {
        canShoot = false; // Disable shooting
        yield return new WaitForSeconds(shootCooldown); // Wait for cooldown duration (1.5 seconds)
        canShoot = true; // Re-enable shooting
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true; // Prevents player double jumping
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!"); // Game Over is triggered when player collides with obstacle
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            gameManager.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.UpdateScore(5); // Score is increased by 5 and game object is destroyed  
            hasPowerUp = true;
            Destroy(other.gameObject); // Makes powerup disappear
            playerAudio.PlayOneShot(powerupConsume, 1.0f); // Eating sound plays when player collides with powerup
        }
    }
}
