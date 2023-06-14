using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi : MonoBehaviour
{
    private Transform hedef; // Mermi hedefi
    public float HizAyarla = 10f; // Mermi hýzý

    public void HedefBelirle(Transform hedefTransform)
    {
        hedef = hedefTransform;
    }

    private void Update()
    {
        if (hedef == null)
        {
            Destroy(gameObject); // Hedef yoksa mermiyi yok et
            return;
        }

        Vector3 hareketYonu = hedef.position - transform.position;
        transform.Translate(hareketYonu.normalized * HizAyarla * Time.deltaTime, Space.World);

        // Mermi hedefe ulaþtýðýnda yok olabilir veya etkileþim yapabilirsiniz
    }
}
