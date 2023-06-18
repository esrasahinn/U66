using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float can = 100f;
    [SerializeField] float maxCan = 100f;
    [SerializeField] Slider canBariSlider; // Can çubuðu Slider bileþeni
    [SerializeField] Transform launchPoint;
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

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
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

    public void HasarAl(int hasar)
    {
        can -= hasar;

        if (canBariSlider != null)
        {
            canBariSlider.value = can;
        }

        if (can <= 0)
        {
            Olum();
        }
        else if (_playerBehaviour != null)
        {
            _playerBehaviour.PlayerTakeDmg(hasar);
        }
    }

    private void Olum()
    {
        Debug.Log("Dusman Oldu");
        // Düþmanýn ölümüyle ilgili yapýlmasý gereken iþlemler buraya eklenebilir.
        Destroy(gameObject); // Düþman nesnesini yok etmek için kullanabilirsiniz.
    }


    void ChasePlayer()
    {
        enemy.SetDestination(player.position);
    }

    void AttackPlayer()
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

            

        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
