using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanMermi : MonoBehaviour
{
    private int mermiHasari;
    [SerializeField] float mermiHizi = 10f; // Mermi hýzý
    [SerializeField] float mermiOmru = 3f; // Mermi ömrü (süresi)
    private Transform hedef; // Mermi hedefi

    public void Ayarla(int hasar, Transform hedefNokta)
    {
        mermiHasari = hasar;
        hedef = hedefNokta;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.HasarAl(mermiHasari); // Oyuncuya hasar ver
        }

        Destroy(gameObject); // Mermiyi yok et
    }

    private void Update()
    {
        if (hedef != null)
        {
            Vector3 yon = (hedef.position - transform.position).normalized;
            transform.position += yon * mermiHizi * Time.deltaTime;
        }
        else
        {
            // Hedef yoksa mermiyi ileri doðru hareket ettir
            transform.position += (transform.up + transform.forward) * mermiHizi * Time.deltaTime;
        }

        Destroy(gameObject, mermiOmru); // Mermiyi belirtilen süre sonra yok et
    }
}