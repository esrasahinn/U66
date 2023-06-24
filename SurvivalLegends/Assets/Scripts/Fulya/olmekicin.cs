using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class olmekicin : MonoBehaviour
{
    UnitHealth unitHealth;
    Animator animator; // Animator bile�eni

    void Start()
    {
        unitHealth = new UnitHealth(100, 100);
        animator = GetComponent<Animator>(); // Animator bile�enini al
    }

    void Update()
    {
        // Burada sa�l�k de�erini kontrol edebilirsiniz
        if (unitHealth.Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (animator != null)
        {
            // "Dead" adl� bir trigger parametresi kullanarak �lme animasyonunu ba�lat�n
            animator.SetBool("Death", true);
        }

        // �l�m animasyonunun tamamlanmas�n� beklemek i�in belirli bir s�re bekleyin
        // Ard�ndan oyun nesnesini yok edebilirsiniz.
        float deathAnimationDuration = 2f; // �l�m animasyonunun s�resi (�rnek olarak 2 saniye)
        Destroy(gameObject, deathAnimationDuration);
    }
}