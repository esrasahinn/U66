using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int attackDamage = 20;
    public float attackRange = 1.5f;

    private Animator animator;
    private ArcherPlayerBehaviour playerBehaviour;
    private bool isAttacking = false;
    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerBehaviour = FindObjectOfType<ArcherPlayerBehaviour>();
    }

    private void Update()
    {
        if (isAttacking || isDead)
        {
            return;
        }

        if (playerBehaviour != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerBehaviour.transform.position);

            if (distanceToPlayer <= attackRange)
            {
                Attack();
            }
            else
            {
                ChasePlayer();
            }
        }
    }

    private void ChasePlayer()
    {
        transform.LookAt(playerBehaviour.transform);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        animator.SetBool("Running", true);
    }

    private void Attack()
    {
        transform.LookAt(playerBehaviour.transform);
        animator.SetBool("Running", false);
        animator.SetBool("Attack", true);
    }

    public void StartAttack()
    {
        isAttacking = true;
    }

    public void StopAttack()
    {
        animator.SetBool("Attack", false);
        isAttacking = false;
    }

    public void Death()
    {
        if (isDead)
        {
            return;
        }

        animator.SetBool("Death", true);
        isDead = true;

        // �l�m animasyonunun s�resini al�n
        AnimationClip deathAnimation = GetAnimationClip("Death");

        if (deathAnimation != null)
        {
            // �l�m animasyonunun s�resini bekle
            Invoke("DestroyEnemy", deathAnimation.length);
        }
        else
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        if (playerBehaviour != null)
        {
            playerBehaviour.PlayerTakeDmg(damage); // Player'a hasar verme i�lemi
        }
    }

    private AnimationClip GetAnimationClip(string clipName)
    {
        // D��man�n animasyon bile�enini al
        AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);

        // �stenen animasyon klibini bul ve d�nd�r
        foreach (AnimatorClipInfo clipInfo in clips)
        {
            if (clipInfo.clip.name == clipName)
            {
                return clipInfo.clip;
            }
        }

        Debug.LogError("EnemyController: Animation clip not found - " + clipName);
        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TakeDamage(attackDamage);
        }
    }
}