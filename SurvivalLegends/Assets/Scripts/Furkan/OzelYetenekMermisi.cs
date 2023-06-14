using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OzelYetenekMermisi : MonoBehaviour
{
    public float hiz = 10f; // Mermi h�z�

    private Transform hedef; // Hedef transformi

    void Update()
    {
        if (hedef != null)
        {
            // Hedefe do�ru ilerleme vekt�r�
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