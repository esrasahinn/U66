using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenBullet : MonoBehaviour
{
    public float freezeDuration = 3f; // Mermi düþmaný dondurma süresi
    private Transform hedef; // Hedef Transform referansý
    public float hedefeGitmeHizi; // Hedefe giderken kullanýlacak hýz deðeri

    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemyAI = other.GetComponent<EnemyController>();
        if (enemyAI != null && other.CompareTag("Dusman")) // Sadece Dusman tag'ine sahip objeleri etkile
        {
            enemyAI.FreezeEnemy(); // Düþmaný dondur
        }

        Destroy(gameObject); // Mermiyi yok et
    }

    public void HedefBelirle(Transform hedefTransform)
    {
        hedef = hedefTransform;

        if (hedef != null)
        {
            Vector3 hedefYonu = hedef.position - transform.position;
            hedefYonu.y = 0f;
            transform.rotation = Quaternion.LookRotation(hedefYonu);
        }
    }

    public void HizAyarla(float hiz)
    {
        hedefeGitmeHizi = hiz;

        if (hedef != null)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * hedefeGitmeHizi;
        }
    }
}