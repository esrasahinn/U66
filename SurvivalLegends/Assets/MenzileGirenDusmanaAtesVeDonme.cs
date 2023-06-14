using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenzileGirenDusmanaAtesVeDonme : MonoBehaviour
{
    public float menzilMesafesi = 10f; // Ateþin menzil mesafesi
    public float donmeHizi = 5f; // Dönme hýzý
    public float atesHizi = 10f; // Ateþ hýzý (saniyede ateþ sayýsý)
    public float ateþEtmeMesafesi = 5f; // Ateþ etme mesafesi
    public float hedefeGitmeHizi = 5f; // Merminin hedefe gitme hýzý
    public float donmeBaslamaMesafesi = 3f; // Dönme mesafesi baþlangýcý
    public float donmeBitisMesafesi = 7f; // Dönme mesafesi bitiþi
    public float mermiOmru = 5f; // Mermi ömrü (saniye)
    public GameObject mermiPrefab; // Merminin prefabý
    public Transform ateþNoktasý; // Ateþin baþlayacaðý nokta

    private GameObject hedef; // Menzile giren düþman hedefi
    private bool dusmanaDonuyor = false; // Düþmana dönme durumu
    private bool menzilde = false; // Menzile girdi durumu
    private float sonrakiAteþZamaný = 0f; // Sonraki ateþ zamanýný takip etmek için kullanýlýr

    private void Update()
    {
        // Hedef yoksa veya hedef menzil dýþýndaysa, yeni bir hedef seç
        if (hedef == null || Vector3.Distance(transform.position, hedef.transform.position) > menzilMesafesi)
        {
            HedefSec();
            dusmanaDonuyor = false; // Düþmana dönme durumunu sýfýrla
            menzilde = false; // Menzile girdi durumunu sýfýrla
        }

        // Hedef seçildiyse ve düþmana dönme mesafesine geldiyse düþmana doðru dön
        if (hedef != null && Vector3.Distance(transform.position, hedef.transform.position) <= donmeBitisMesafesi)
        {
            if (!menzilde && Vector3.Distance(transform.position, hedef.transform.position) <= donmeBaslamaMesafesi)
            {
                dusmanaDonuyor = true; // Düþmana dönme durumunu etkinleþtir
                menzilde = true; // Menzile girdi durumunu etkinleþtir
            }
            DusmanaDon();
        }
        else
        {
            dusmanaDonuyor = false; // Dönme durumunu sýfýrla
            menzilde = false; // Menzilden çýktý durumunu sýfýrla
        }

        // Hedef seçildiyse, ateþ etme mesafesine geldiyse ve ateþ zamaný geldiyse ateþ et
        if (hedef != null && Vector3.Distance(transform.position, hedef.transform.position) <= ateþEtmeMesafesi && Time.time >= sonrakiAteþZamaný)
        {
            AtesEt(); // Ateþ etme fonksiyonunu çaðýrýr
            dusmanaDonuyor = true; // Düþmana dönme durumunu etkinleþtir

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

    void DusmanaDon()
    {
        Vector3 hedefYonu = hedef.transform.position - transform.position;
        hedefYonu.y = 0f; // Y ekseni üzerinde dönmemek için sýfýrla
        Quaternion hedefRotasyonu = Quaternion.LookRotation(hedefYonu);
        transform.rotation = Quaternion.Slerp(transform.rotation, hedefRotasyonu, donmeHizi * Time.deltaTime);
    }

    void AtesEt()
    {
        GameObject mermi = Instantiate(mermiPrefab, ateþNoktasý.position, ateþNoktasý.rotation);
        Mermi mermiScript = mermi.GetComponent<Mermi>(); // Mermi scriptini al
        mermiScript.HedefBelirle(hedef.transform); // Mermi scriptindeki HedefBelirle fonksiyonunu çaðýr
       // mermiScript.HizAyarla(hedefeGitmeHizi); // Mermi hýzýný ayarla

        Destroy(mermi, mermiOmru); // Mermi objesini belirli bir süre sonra yok et
    }
}