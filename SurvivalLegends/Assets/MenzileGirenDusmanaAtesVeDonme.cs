using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenzileGirenDusmanaAtesVeDonme : MonoBehaviour
{
    public float menzilMesafesi = 10f; // Ate�in menzil mesafesi
    public float donmeHizi = 5f; // D�nme h�z�
    public float atesHizi = 10f; // Ate� h�z� (saniyede ate� say�s�)
    public float ate�EtmeMesafesi = 5f; // Ate� etme mesafesi
    public float hedefeGitmeHizi = 5f; // Merminin hedefe gitme h�z�
    public float donmeBaslamaMesafesi = 3f; // D�nme mesafesi ba�lang�c�
    public float donmeBitisMesafesi = 7f; // D�nme mesafesi biti�i
    public float mermiOmru = 5f; // Mermi �mr� (saniye)
    public GameObject mermiPrefab; // Merminin prefab�
    public Transform ate�Noktas�; // Ate�in ba�layaca�� nokta

    private GameObject hedef; // Menzile giren d��man hedefi
    private bool dusmanaDonuyor = false; // D��mana d�nme durumu
    private bool menzilde = false; // Menzile girdi durumu
    private float sonrakiAte�Zaman� = 0f; // Sonraki ate� zaman�n� takip etmek i�in kullan�l�r

    private void Update()
    {
        // Hedef yoksa veya hedef menzil d���ndaysa, yeni bir hedef se�
        if (hedef == null || Vector3.Distance(transform.position, hedef.transform.position) > menzilMesafesi)
        {
            HedefSec();
            dusmanaDonuyor = false; // D��mana d�nme durumunu s�f�rla
            menzilde = false; // Menzile girdi durumunu s�f�rla
        }

        // Hedef se�ildiyse ve d��mana d�nme mesafesine geldiyse d��mana do�ru d�n
        if (hedef != null && Vector3.Distance(transform.position, hedef.transform.position) <= donmeBitisMesafesi)
        {
            if (!menzilde && Vector3.Distance(transform.position, hedef.transform.position) <= donmeBaslamaMesafesi)
            {
                dusmanaDonuyor = true; // D��mana d�nme durumunu etkinle�tir
                menzilde = true; // Menzile girdi durumunu etkinle�tir
            }
            DusmanaDon();
        }
        else
        {
            dusmanaDonuyor = false; // D�nme durumunu s�f�rla
            menzilde = false; // Menzilden ��kt� durumunu s�f�rla
        }

        // Hedef se�ildiyse, ate� etme mesafesine geldiyse ve ate� zaman� geldiyse ate� et
        if (hedef != null && Vector3.Distance(transform.position, hedef.transform.position) <= ate�EtmeMesafesi && Time.time >= sonrakiAte�Zaman�)
        {
            AtesEt(); // Ate� etme fonksiyonunu �a��r�r
            dusmanaDonuyor = true; // D��mana d�nme durumunu etkinle�tir

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

    void DusmanaDon()
    {
        Vector3 hedefYonu = hedef.transform.position - transform.position;
        hedefYonu.y = 0f; // Y ekseni �zerinde d�nmemek i�in s�f�rla
        Quaternion hedefRotasyonu = Quaternion.LookRotation(hedefYonu);
        transform.rotation = Quaternion.Slerp(transform.rotation, hedefRotasyonu, donmeHizi * Time.deltaTime);
    }

    void AtesEt()
    {
        GameObject mermi = Instantiate(mermiPrefab, ate�Noktas�.position, ate�Noktas�.rotation);
        Mermi mermiScript = mermi.GetComponent<Mermi>(); // Mermi scriptini al
        mermiScript.HedefBelirle(hedef.transform); // Mermi scriptindeki HedefBelirle fonksiyonunu �a��r
       // mermiScript.HizAyarla(hedefeGitmeHizi); // Mermi h�z�n� ayarla

        Destroy(mermi, mermiOmru); // Mermi objesini belirli bir s�re sonra yok et
    }
}