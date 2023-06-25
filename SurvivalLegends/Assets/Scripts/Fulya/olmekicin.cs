using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class olmekicin : MonoBehaviour
{
    UnitHealth unitHealth;
    Animator animator; // Animator bileþeni

    void Start()
    {
        unitHealth = new UnitHealth(100, 100);
        animator = GetComponent<Animator>(); // Animator bileþenini al
    }

    void Update()
    {
        // Burada saðlýk deðerini kontrol edebilirsiniz
        if (unitHealth.Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (animator != null)
        {
            // "Dead" adlý bir trigger parametresi kullanarak ölme animasyonunu baþlatýn
            animator.SetBool("Death", true);
        }

        // Ölüm animasyonunun tamamlanmasýný beklemek için belirli bir süre bekleyin
        // Ardýndan oyun nesnesini yok edebilirsiniz.
        float deathAnimationDuration = 2f; // Ölüm animasyonunun süresi (örnek olarak 2 saniye)
        Destroy(gameObject, deathAnimationDuration);
    }
}