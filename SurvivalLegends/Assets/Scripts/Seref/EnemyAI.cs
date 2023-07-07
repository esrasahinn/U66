using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    [SerializeField] float can = 100f;
    [SerializeField] float maxCan = 100f;
    [SerializeField] Slider canBariSlider;
    [SerializeField] Transform launchPoint;
    public int currentHealth;
    private PlayerBehaviour _playerBehaviour;
    private NavMeshAgent enemy;
    public Transform player;
    public LayerMask whatIsPlayer;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10.0f;
    public float attackCooldown;
    private bool alreadyAttacked;
    public float attackRange = 10.0f;
    private bool inAttackRange;
    DropCoin coinScript;
    private bool isFrozen;
    private Animator enemyAnimator;

    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Death = Animator.StringToHash("Death");

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
        coinScript = GetComponent<DropCoin>();
        enemyAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        inAttackRange = distanceToPlayer <= attackRange;

        if (inAttackRange)
        {
            AttackPlayer();
        }
        else
        {
            ChasePlayer();
        }
    }

    public void HasarAl(float mermiHasar)
    {
        can -= mermiHasar;
        currentHealth = (int)can;

        if (canBariSlider != null)
        {
            canBariSlider.value = can;
        }

        if (FloatingTextPrefab)
        {
            ShowFloatingText(mermiHasar);
        }

        if (can <= 0)
        {
            Olum();
        }
        else if (_playerBehaviour != null)
        {
            _playerBehaviour.PlayerTakeDmg((int)mermiHasar);
        }
    }

    void ShowFloatingText(float mermiHasar)
    {
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = mermiHasar.ToString();
    }

    private void Olum()
    {
        Debug.Log("Dusman Oldu");
        enemyAnimator.SetTrigger(Death);
        Destroy(gameObject, 1f);
        coinScript.CoinDrop();
        expController expControllerScript = FindObjectOfType<expController>();
        if (expControllerScript != null)
        {
            expControllerScript.UpdateExpBar();
        }
    }

    void ChasePlayer()
    {
        if (!isFrozen)
        {
            enemy.SetDestination(player.position);
            enemyAnimator.SetBool(Running, true);
        }
    }

    void AttackPlayer()
    {
        if (!isFrozen)
        {
            enemy.SetDestination(transform.position);

            if (!alreadyAttacked)
            {
                GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);

                Vector3 direction = (player.position - transform.position).normalized;

                Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
                projectileRb.velocity = direction * projectileSpeed;

                ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
                projectileController.Initialize(transform.position);
                transform.LookAt(player);
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), attackCooldown);

                enemyAnimator.SetTrigger(Attack);
            }
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void FreezeEnemy()
    {
        isFrozen = true;
        StartCoroutine(UnfreezeEnemy());
    }

    private IEnumerator UnfreezeEnemy()
    {
        yield return new WaitForSeconds(3f);
        isFrozen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Katana"))
        {
            Katana katana = other.GetComponent<Katana>();
            if (katana != null)
            {
                HasarAl(katana.damageAmount);
            }
        }
    }
}