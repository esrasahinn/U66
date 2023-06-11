using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenzileGirenDusmanaAtesEt : MonoBehaviour
{
    public float menzilMesafesi = 10f; // Ateþin menzil mesafesi
    public float atesHizi = 10f; // Ateþ hýzý (saniyede ateþ sayýsý)
    public GameObject mermiPrefab; // Merminin prefabý
    public Transform ateþNoktasý; // Ateþin baþlayacaðý nokta

    private GameObject hedef; // Menzile giren düþman hedefi
    private float sonrakiAteþZamaný = 0f; // Sonraki ateþ zamanýný takip etmek için kullanýlýr

    private void Update()
    {
        // Hedef yoksa veya hedef menzil dýþýndaysa, yeni bir hedef seç
        if (hedef == null || Vector3.Distance(transform.position, hedef.transform.position) > menzilMesafesi)
        {
            HedefSec();
        }

        // Hedef seçildiyse ve ateþ zamaný geldiyse ateþ et
        if (hedef != null && Time.time >= sonrakiAteþZamaný)
        {
            AtesEt(); // Ateþ etme fonksiyonunu çaðýrýr

            sonrakiAteþZamaný = Time.time + 1f / atesHizi; // Sonraki ateþ zamanýný günceller
        }
    }

    void HedefSec()
    {
        // Tüm düþmanlarý al
        GameObject[] dusmanlar = GameObject.FindGameObjectsWithTag("Dusman");

        // En yakýn düþmaný bul ve hedef olarak seç
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

        // Hedefi güncelle
        hedef = enYakinDusman;
    }

    void AtesEt()
    {
        Instantiate(mermiPrefab, ateþNoktasý.position, ateþNoktasý.rotation); // Merminin prefabýndan bir tane oluþturur
    }
}
