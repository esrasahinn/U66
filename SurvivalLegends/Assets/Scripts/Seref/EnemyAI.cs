using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform launchPoint;
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

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
