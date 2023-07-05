using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    [SerializeField] float can = 100f;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] Slider healthSlider; // Can �ubu�u Slider bile�eni
    [SerializeField] Transform player;
    [SerializeField] float attackCooldown = 2.0f;
    [SerializeField] float attackRange = 2.0f;
    [SerializeField] int meleeDamage = 10;
    public int currentHealth;
    private NavMeshAgent enemy;
    private bool alreadyAttacked;
    private bool inAttackRange;
    private Animator animator;
    private bool isDead;
    private bool isFrozen;

    private void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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
            enemy.isStopped = true; // Karakteri durdur
            animator.SetBool("Running", false);

            if (inAttackRange)
            {
                AttackPlayer();
            }
            return;
        }

        if (inAttackRange)
        {
            AttackPlayer();
        }
        else
        {
            enemy.isStopped = false; // Karakteri hareket ettir
            ChasePlayer();
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
        Debug.Log("D��man �ld�.");
        isDead = true;
        // �l�m animasyonunu oynatmak veya di�er �l�m i�lemlerini burada yapabilirsiniz.
        animator.SetTrigger("Death");
        enemy.enabled = false;
        // D��man� yok etmek veya etraf�na d��en e�yalar� burada i�leyebilirsiniz.
        Destroy(gameObject, 2.0f); // �ki saniye sonra d��man nesnesini yok etmek i�in kullanabilirsiniz.
    }

    void ChasePlayer()
    {
        if (!alreadyAttacked)
        {
            enemy.SetDestination(player.position);
            animator.SetBool("Running", true);
            animator.SetBool("Attack", false);
        }
        else
        {
            enemy.SetDestination(transform.position);
            animator.SetBool("Running", false);
            animator.SetBool("Attack", false);
        }
    }

    public void AttackPlayer()
    {
        enemy.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            animator.SetBool("Running", false);
            animator.SetBool("Attack", true);

            // Sald�r� animasyonu oynat�labilir veya di�er sald�r� i�lemleri burada yap�labilir.
            // Oyuncuya hasar vermek i�in ArcherPlayerBehaviour veya PlayerBehaviour bile�enini �a��rabilirsiniz.

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

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
        else
        {
            animator.SetBool("Running", true);
            animator.SetBool("Attack", false);
        }
    }

    public void FreezeEnemy()
    {
        isFrozen = true;
        alreadyAttacked = false;
        animator.SetBool("Running", false);
        enemy.isStopped = true; // Karakteri durdur
        StartCoroutine(UnfreezeEnemy());
    }

    private IEnumerator UnfreezeEnemy()
    {
        yield return new WaitForSeconds(3f); // Dondurma s�resi (3 saniye) beklenir
        isFrozen = false;
        enemy.isStopped = false; // Karakteri hareket ettir
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
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