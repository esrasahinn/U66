using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenzileGirenDusmanaAtesVeDonme : MonoBehaviour
{
    public float menzilMesafesi = 10f;
    public float donmeHizi = 5f;
    public float atesHizi = 10f;
    public float ateþEtmeMesafesi = 5f;
    public float hedefeGitmeHizi = 5f;
    public float donmeBaslamaMesafesi = 3f;
    public float donmeBitisMesafesi = 7f;
    public float mermiOmru = 5f;
    public GameObject mermiPrefab;
    public Transform ateþNoktasý;
    public Transform ozelYetenekAteþNoktasý; // Özel yetenek ateþ noktasý transformu
    public float ozelYetenekAraligi = 10f;
    public float ozelYetenekAtesHizi = 5f;
    public GameObject ozelYetenekPrefab; // Özel yetenek prefabý

    private GameObject hedef;
    private bool dusmanaDonuyor = false;
    private bool menzilde = false;
    private float sonrakiAteþZamaný = 0f;
    private bool ozelYetenekAktif = false;
    private float sonrakiOzelYetenekZamani = 0f;

    [SerializeField] private Button ozelYetenekButonu;

    private void Start()
    {
        // Özel yetenek butonuna týklama olayýný dinle
        ozelYetenekButonu.onClick.AddListener(AktifEtOzelYetenek);
    }

    private void Update()
    {
        if (hedef == null || Vector3.Distance(transform.position, hedef.transform.position) > menzilMesafesi)
        {
            HedefSec();
            dusmanaDonuyor = false;
            menzilde = false;
        }

        if (hedef != null && Vector3.Distance(transform.position, hedef.transform.position) <= donmeBitisMesafesi)
        {
            if (!menzilde && Vector3.Distance(transform.position, hedef.transform.position) <= donmeBaslamaMesafesi)
            {
                dusmanaDonuyor = true;
                menzilde = true;
            }
            DusmanaDon();
        }
        else
        {
            dusmanaDonuyor = false;
            menzilde = false;
        }

        if (hedef != null && Vector3.Distance(transform.position, hedef.transform.position) <= ateþEtmeMesafesi && Time.time >= sonrakiAteþZamaný)
        {
            if (ozelYetenekAktif)
            {
                AtesEt(ozelYetenekAteþNoktasý); // Özel yetenek ateþ noktasýný kullanarak ateþ et
                sonrakiAteþZamaný = Time.time + 1f / ozelYetenekAtesHizi;
            }
            else
            {
                AtesEt(ateþNoktasý); // Normal ateþ noktasýný kullanarak ateþ et
                sonrakiAteþZamaný = Time.time + 1f / atesHizi;
            }

            dusmanaDonuyor = true;
        }

        if (Time.time >= sonrakiOzelYetenekZamani)
        {
            ozelYetenekAktif = false;
        }
    }

    void HedefSec()
    {
        GameObject[] dusmanlar = GameObject.FindGameObjectsWithTag("Dusman");

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

        hedef = enYakinDusman;
    }

    void DusmanaDon()
    {
        Vector3 hedefYonu = hedef.transform.position - transform.position;
        hedefYonu.y = 0f;
        Quaternion hedefRotasyonu = Quaternion.LookRotation(hedefYonu);
        transform.rotation = Quaternion.Slerp(transform.rotation, hedefRotasyonu, donmeHizi * Time.deltaTime);
    }

    void AtesEt(Transform atesNoktasi)
    {
        GameObject mermi = Instantiate(mermiPrefab, atesNoktasi.position, atesNoktasi.rotation);
        Mermi mermiScript = mermi.GetComponent<Mermi>();
        mermiScript.HedefBelirle(hedef.transform);
       // mermiScript.HizAyarla(hedefeGitmeHizi);

        Destroy(mermi, mermiOmru);
    }

    public void AktifEtOzelYetenek()
    {

        if (!ozelYetenekAktif)
        {

            ozelYetenekAktif = true;
            sonrakiOzelYetenekZamani = Time.time + ozelYetenekAraligi;

            Instantiate(ozelYetenekPrefab, transform.position, transform.rotation);
        }
    }
}