using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Added for UI functionality

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce = 15.0f;
    public float gravityModifier = 2.0f;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool hasPowerUp;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip shootSound;
    public AudioClip gameOverSound;
    public AudioClip powerupConsume;
    public AudioSource playerAudio;
    public GameObject projectilePrefab;
    public GameManager gameManager;
    public TextMeshProUGUI gameOverText;

    private ProjectilePool projectilePool;
    private bool canShoot = true;
    [SerializeField] private float shootCooldown = 1.5f;

    // Cooldown UI variables
    [SerializeField] private Image cooldownImage; // Assign in Inspector

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        projectilePool = GameObject.Find("ProjectilePool").GetComponent<ProjectilePool>();

        // Initialize cooldown UI
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 0; // Start empty
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !gameOver)
        {
            Shoot();
        }
    }

    public void Jump()
    {
        if (isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * 15, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    public void Shoot()
    {
        if (!canShoot || gameOver) return;

        LaunchProjectile();
        StartCoroutine(ShootCooldownRoutine());

        // Reset cooldown UI when shooting
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 1; // Start full
        }
    }

    private void LaunchProjectile()
    {
        GameObject projectile = projectilePool.GetProjectile();
        projectile.transform.position = transform.position;
        projectile.transform.rotation = projectilePrefab.transform.rotation;

        if (playerAudio != null && shootSound != null)
        {
            playerAudio.PlayOneShot(shootSound, 1.0f);
        }
    }

    private IEnumerator ShootCooldownRoutine()
    {
        canShoot = false;
        float timer = 0;

        while (timer < shootCooldown)
        {
            timer += Time.deltaTime;

            // Update cooldown UI
            if (cooldownImage != null)
            {
                cooldownImage.fillAmount = 1 - (timer / shootCooldown);
            }

            yield return null;
        }

        canShoot = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle") && !gameOver)
        {
            HandleGameOver();
        }
    }

    private void HandleGameOver()
    {
        Debug.Log("Game Over!");
        gameOver = true;
        gameManager.GameOver();
        playerAudio.Stop();

        if (playerAudio != null && gameOverSound != null)
        {
            playerAudio.PlayOneShot(gameOverSound, 1.0f);
        }

        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", 1);
        explosionParticle.Play();
        dirtParticle.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            gameManager.UpdateScore(5);
            hasPowerUp = true;
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(powerupConsume, 1.0f);
        }
    }
}