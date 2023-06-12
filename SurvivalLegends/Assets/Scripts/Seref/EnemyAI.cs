using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour

{
    private Animator animator;
    public NavMeshAgent enemy;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public GameObject projectile;
    public float projectileSpeed = 10.0f;
    public float attackCooldown;
    public bool alreadyAttacked;
    public float attackRange = 10.0f;
    public bool inAttackRange;

    

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (inAttackRange) { AttackPlayer(); }
        if (!inAttackRange) { ChasePlayer(); }
    }

    void ChasePlayer()
    {
        enemy.SetDestination(player.position);
        animator.SetFloat("Speed", 1);
    }

    void AttackPlayer()
    {
        enemy.SetDestination(transform.position);
        animator.SetFloat("Speed", 0);
        animator.SetBool("AttackRange", true);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody projectileRb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            projectileRb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
            alreadyAttacked = true;
            animator.SetBool("InAttackCD", true);
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
        animator.SetBool("InAttackCD", false);
    }
}
