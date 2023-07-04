using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasarVerici2 : MonoBehaviour
{
    public int hasarMiktari = 10; // Verilecek hasar miktarý

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Player'a hasar ver
            ArcherPlayerBehaviour oyuncuKontrol = collision.gameObject.GetComponent<ArcherPlayerBehaviour>();
            if (oyuncuKontrol != null)
            {
                oyuncuKontrol.PlayerTakeDmg(hasarMiktari);
            }
        }
    }
}
