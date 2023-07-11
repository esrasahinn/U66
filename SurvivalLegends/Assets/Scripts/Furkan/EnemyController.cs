using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    [SerializeField] float can = 100f;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] Slider healthSlider;
    [SerializeField] Transform player;
    [SerializeField] float attackRange = 2.0f;
    [SerializeField] float detectionRange = 10.0f; // Fark etme menzili
    [SerializeField] int meleeDamage = 10;
    [SerializeField] float expAmount = 0.1f; // Düþmandan kazanýlan deneyim miktarý
    public int currentHealth;
    private NavMeshAgent enemy;
    private bool inAttackRange;
    private Animator animator;
    private bool isDead;
    private bool isFrozen;
    private bool hasDetectedPlayer; // Fark edildi mi kontrolü
    private LevelManager levelManager; // LevelManager bileþeni referansý
    DropCoin coinScript;

    private void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        coinScript = GetComponent<DropCoin>();

        // LevelManager bileþenini bulma
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    private void Update()
    {
        if (isDead)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        inAttackRange = distanceToPlayer <= attackRange;

        if (isFrozen)
        {
            enemy.isStopped = true;
            animator.SetBool("Running", false);

            if (inAttackRange)
            {
                animator.SetBool("Attack", true);
                AttackPlayer();
            }
            else
            {
                animator.SetBool("Attack", false);
            }
            return;
        }

        if (inAttackRange)
        {
            animator.SetBool("Attack", true);
            AttackPlayer();
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
        enemy.SetDestination(player.position);
        animator.SetBool("Running", true);
    }

    public void AttackPlayer()
    {
        enemy.SetDestination(transform.position);
        transform.LookAt(player);

        animator.SetBool("Running", false);
        animator.SetBool("Attack", true);

        ArcherPlayerBehaviour archerPlayerHealth = player.GetComponent<ArcherPlayerBehaviour>();
        if (archerPlayerHealth != null)
        {
            archerPlayerHealth.PlayerTakeDmg(meleeDamage);
        }

        PlayerBehaviour playerHealth = player.GetComponent<PlayerBehaviour>();
        if (playerHealth != null)
        {
            playerHealth.PlayerTakeDmg(meleeDamage);
        }

        Invoke(nameof(CompleteAttackAnimation), 2.0f);
    }

    void CompleteAttackAnimation()
    {
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
        if (other.CompareTag("Katana"))
        {
            Katana katana = other.GetComponent<Katana>();
            if (katana != null)
            {
                TakeDamage(katana.damageAmount);
            }
        }
    }
}
