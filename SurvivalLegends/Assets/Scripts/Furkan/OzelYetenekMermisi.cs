using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OzelYetenekMermisi : MonoBehaviour
{
    public float hiz = 10f; // Mermi hýzý

    private Transform hedef; // Hedef transformi

    void Update()
    {
        if (hedef != null)
        {
            // Hedefe doðru ilerleme vektörü
            Vector3 hareketYonu = hedef.position - transform.position;
            hareketYonu.Normalize();

            // Mermiyi ileri hareket ettir
            transform.position += hareketYonu * hiz * Time.deltaTime;
        }
    }

    // Hedefi belirleme fonksiyonu
    public void HedefBelirle(Transform hedefTransform)
    {
        hedef = hedefTransform;
    }
}