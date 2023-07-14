using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RangedEnemyController : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    public float maxHealth = 100f;
    public Slider healthSlider;
    [SerializeField] string playerTag = "Player"; // Oyuncu tagi
    private GameObject player;
    public float attackRange = 10.0f;
    public float attackCooldown = 2.0f;
    public float detectionRange = 15.0f; // Fark etme menzili
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 10f;
    public float projectileDestroyTime = 2.0f;
    public int rangedDamage = 10;
    public int currentHealth;
    private NavMeshAgent enemy;
    private Animator animator;
    private bool isDead;
    private bool isFrozen;
    private bool hasDetectedPlayer; // Fark edildi mi kontrolü
    [SerializeField] float expAmount = 0.1f;
    private LevelManager levelManager;
    DropCoin coinScript;
    AudioSource audiosource;
    private float nextAttackTime;

    private void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        coinScript = GetComponent<DropCoin>();
    }

    private void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        player = GameObject.FindGameObjectWithTag(playerTag); // Oyuncuyu tagi kullanarak bulma
        audiosource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isDead)
            return;

        if (isFrozen)
        {
            enemy.isStopped = true;
            animator.SetBool("Running", false);
            animator.SetBool("Attack", false);
            return;
        }

        if (player == null)
        {
            Debug.LogWarning("Oyuncu bulunamadý.");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= attackRange)
        {
            animator.SetBool("Running", false);
            if (Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + attackCooldown;
                AttackPlayer();
            }
            else
            {
                animator.SetBool("Attack", false);
            }
        }
        else
        {
            animator.SetBool("Attack", false);
            enemy.isStopped = false;

            if (distanceToPlayer <= detectionRange || hasDetectedPlayer) // Düþman fark etme menziline girdiðinde veya oyuncu fark edildikten sonra takip eder
            {
                hasDetectedPlayer = true;
                ChasePlayer();
            }
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (isDead)
            return;

        healthSlider.value -= damage;

        if (FloatingTextPrefab)
        {
            ShowFloatingText(damage);
        }

        if (healthSlider.value <= 0)
        {
            Die();
            levelManager.EnemyDied();
        }
    }

    void ShowFloatingText(int damage)
    {
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = damage.ToString();
    }

    void Die()
    {
        Debug.Log("Düþman öldü.");
        audiosource.Play();
        isDead = true;
        animator.SetTrigger("Death");
        enemy.enabled = false;
        coinScript.CoinDrop();
        Destroy(gameObject, 2.0f);
        expController expControllerScript = FindObjectOfType<expController>();
        if (expControllerScript != null)
        {
            expControllerScript.UpdateExpBar(expAmount);
        }
        FindObjectOfType<LevelManager>().EnemyDied();

        // Düþman öldüðünde LevelManager'a bilgi gönderme
        levelManager.EnemyDied();

        // Düþman öldüðünde EndCube bileþenine bilgi gönderme
        EndCube endCube = FindObjectOfType<EndCube>();
        if (endCube != null)
        {
            endCube.EnemyDied();
        }
    }

    void ChasePlayer()
    {
        enemy.SetDestination(player.transform.position);
        animator.SetBool("Running", true);
    }

    public void AttackPlayer()
    {
        enemy.SetDestination(transform.position);
        transform.LookAt(player.transform);

        animator.SetBool("Running", false);
        animator.SetBool("Attack", true);

        StartCoroutine(ShootProjectile());
    }


    IEnumerator ShootProjectile()
    {
        yield return new WaitForSeconds(0.5f);

        // Oyuncunun pozisyonunu hesapla
        Vector3 playerPosition = new Vector3(player.transform.position.x, projectileSpawnPoint.position.y, player.transform.position.z);
        // Oyuncuya doðru bir vektör oluþtur
        Vector3 directionToPlayer = playerPosition - projectileSpawnPoint.position;
        // Yatay rotasyonu ayarla
        Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer, Vector3.up);

        // Okun rotasyonunu ayarla
        projectileSpawnPoint.rotation = rotationToPlayer;

        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, rotationToPlayer);
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.velocity = projectileSpawnPoint.forward * projectileSpeed;

        Destroy(projectile, projectileDestroyTime);

        yield return new WaitForSeconds(attackCooldown - 0.5f);

        animator.SetBool("Attack", false);
    }

    public void FreezeEnemy()
    {
        isFrozen = true;
        animator.SetBool("Running", false);
        enemy.isStopped = true;
        StartCoroutine(UnfreezeEnemy());
    }

    private IEnumerator UnfreezeEnemy()
    {
        yield return new WaitForSeconds(3f);
        isFrozen = false;
        enemy.isStopped = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (!hasDetectedPlayer)
            {
                hasDetectedPlayer = true;
                ChasePlayer();
            }
        }
        else if (other.CompareTag("Katana"))
        {
            Katana katana = other.GetComponent<Katana>();
            if (katana != null)
            {
                TakeDamage(katana.damageAmount);
            }
        }
    }
}
