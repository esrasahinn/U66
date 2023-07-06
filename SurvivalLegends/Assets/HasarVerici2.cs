using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasarVerici2 : MonoBehaviour
{
    public int vurulanHasar = 10;
    public float hasarGecikmesi = 1f; // Hasar verme gecikmesi süresi

    private bool hasarVerebilecekDurum = true; // Hasar verebilecek durumu takip etmek için bir bayrak

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hasarVerebilecekDurum)
        {
            ArcherPlayerBehaviour playerBehaviour = other.GetComponent<ArcherPlayerBehaviour>();
            if (playerBehaviour != null)
            {
                playerBehaviour.PlayerTakeDmg(vurulanHasar);
                hasarVerebilecekDurum = false; // Hasar verme durumunu geçici olarak devre dýþý býrak
                Invoke("ResetHasarDurumu", hasarGecikmesi); // Belirtilen süre sonunda hasar verme durumunu sýfýrla
            }
        }
    }

    private void ResetHasarDurumu()
    {
        hasarVerebilecekDurum = true; // Hasar verme durumunu tekrar etkinleþtir
    }
}
