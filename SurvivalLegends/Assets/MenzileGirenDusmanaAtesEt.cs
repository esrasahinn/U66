using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenzileGirenDusmanaAtesEt : MonoBehaviour
{
    public float menzilMesafesi = 10f; // Ate�in menzil mesafesi
    public float atesHizi = 10f; // Ate� h�z� (saniyede ate� say�s�)
    public GameObject mermiPrefab; // Merminin prefab�
    public Transform ate�Noktas�; // Ate�in ba�layaca�� nokta

    private GameObject hedef; // Menzile giren d��man hedefi
    private float sonrakiAte�Zaman� = 0f; // Sonraki ate� zaman�n� takip etmek i�in kullan�l�r

    private void Update()
    {
        // Hedef yoksa veya hedef menzil d���ndaysa, yeni bir hedef se�
        if (hedef == null || Vector3.Distance(transform.position, hedef.transform.position) > menzilMesafesi)
        {
            HedefSec();
        }

        // Hedef se�ildiyse ve ate� zaman� geldiyse ate� et
        if (hedef != null && Time.time >= sonrakiAte�Zaman�)
        {
            AtesEt(); // Ate� etme fonksiyonunu �a��r�r

            sonrakiAte�Zaman� = Time.time + 1f / atesHizi; // Sonraki ate� zaman�n� g�nceller
        }
    }

    void HedefSec()
    {
        // T�m d��manlar� al
        GameObject[] dusmanlar = GameObject.FindGameObjectsWithTag("Dusman");

        // En yak�n d��man� bul ve hedef olarak se�
        float enYakinMesafe = Mathf.Infinity;
        GameObject enYakinDusman = null;

        foreach (GameObject dusman in dusmanlar)
        {
            float mesafe = Vector3.Distance(transform.position, dusman.transform.position);

            if (mesafe < enYakinMesafe)
            {
                enYakinMesafe = mesafe;
                enYakinDusman = dusman;
            }
        }

        // Hedefi g�ncelle
        hedef = enYakinDusman;
    }

    void AtesEt()
    {
        Instantiate(mermiPrefab, ate�Noktas�.position, ate�Noktas�.rotation); // Merminin prefab�ndan bir tane olu�turur
    }
}
