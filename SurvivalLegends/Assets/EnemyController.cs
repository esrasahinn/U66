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
    [SerializeField] int meleeDamage = 10;
    public int currentHealth;
    private NavMeshAgent enemy;
    private bool inAttackRange;
    private Animator animator;
    private bool isDead;
    private bool isFrozen;
    private HasarVerici2 hasarVerici2;

    private void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        hasarVerici2 = GetComponentInChildren<HasarVerici2>();
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
        Debug.Log("Düþman öldü.");
        isDead = true;
        animator.SetTrigger("Death");
        enemy.enabled = false;
        Destroy(gameObject, 2.0f);
        expController expControllerScript = FindObjectOfType<expController>();
        if (expControllerScript != null)
        {
            expControllerScript.UpdateExpBar();
        }
    }

    void ChasePlayer()
    {
        enemy.SetDestination(player.position);
        animator.SetBool("Attack", false);

        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                animator.SetBool("Running", false);
                return;
            }
        }

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