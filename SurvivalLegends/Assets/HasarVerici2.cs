using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasarVerici2 : MonoBehaviour
{
    public int vurulanHasar = 10;
    public float hasarGecikmesi = 1f; // Hasar verme gecikmesi s�resi

    private bool hasarVerebilecekDurum = true; // Hasar verebilecek durumu takip etmek i�in bir bayrak

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hasarVerebilecekDurum)
        {
            ArcherPlayerBehaviour playerBehaviour = other.GetComponent<ArcherPlayerBehaviour>();
            if (playerBehaviour != null)
            {
                playerBehaviour.PlayerTakeDmg(vurulanHasar);
                hasarVerebilecekDurum = false; // Hasar verme durumunu ge�ici olarak devre d��� b�rak
                Invoke("ResetHasarDurumu", hasarGecikmesi); // Belirtilen s�re sonunda hasar verme durumunu s�f�rla
            }
        }
    }

    private void ResetHasarDurumu()
    {
        hasarVerebilecekDurum = true; // Hasar verme durumunu tekrar etkinle�tir
    }
}
