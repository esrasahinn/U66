using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenzileGirenDusmanaAtesVeDonme : MonoBehaviour
{
    [SerializeField] float menzilMesafesi = 10f;
    [SerializeField] float donmeHizi = 5f;
    [SerializeField] float atesHizi = 10f;
    [SerializeField] float maxAtesHizi = 80f;
    [SerializeField] float minAtesHizi = 1f;
    [SerializeField] float atesEtmeMesafesi = 5f;
    [SerializeField] float hedefeGitmeHizi = 5f;
    [SerializeField] float donmeBaslamaMesafesi = 3f;
    [SerializeField] float donmeBitisMesafesi = 7f;
    [SerializeField] float mermiOmru = 5f;

    public GameObject mermiPrefab;
    public GameObject ozelYetenekPrefab;
    public Transform atesNoktasi;

    private GameObject hedef;
    private bool dusmanaDonuyor = false;
    private bool menzilde = false;
    private float sonrakiAtesZamani = 0f;

   
    private bool attackInProgress = false;
   
    private bool ozelYetenekAktif = false;
    private GameObject eskiPrefab;
    private float ozelYetenekZamani = 0f;

    private PlayerBehaviour playerBehaviour; // PlayerBehaviour scriptine eri�mek i�in referans

    internal void AddAtesHizi(float boostAmt)
    {
        atesHizi += boostAmt;
        atesHizi = Mathf.Clamp(atesHizi, minAtesHizi, maxAtesHizi);
    }

    private void Start()
    {
        playerBehaviour = GetComponent<PlayerBehaviour>(); // PlayerBehaviour componentini al
    }

    private void Update()
    {
        if (playerBehaviour._health <= 0) // Can de�eri 0 ise ate� etmeyi ve d�nmeyi durdur
            return;

        if (hedef == null || Vector3.Distance(transform.position, hedef.transform.position) > menzilMesafesi)
        {
            HedefSec();
            dusmanaDonuyor = false;
            menzilde = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!ozelYetenekAktif)
            {
                ozelYetenekAktif = true;
                ozelYetenekZamani = Time.time; // �zel yetenek s�resini ba�lat
                OzelYetenek();
            }
        }

        if (ozelYetenekAktif)
        {
            if (Time.time >= ozelYetenekZamani + 0.5f) // �zel yetenek s�resi kontrol ediliyor
            {
                GeriDon();
            }
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

        if (hedef != null && Vector3.Distance(transform.position, hedef.transform.position) <= atesEtmeMesafesi && Time.time >= sonrakiAtesZamani)
        {
            AtesEt();
            dusmanaDonuyor = true;

            sonrakiAtesZamani = Time.time + 1f / atesHizi;
        }
    }

    public void HedefSec()
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

    void AtesEt()
    {
        GameObject mermi = Instantiate(mermiPrefab, atesNoktasi.position, atesNoktasi.rotation);
        Mermi mermiScript = mermi.GetComponent<Mermi>();
        mermiScript.HedefBelirle(hedef.transform);
        mermiScript.HizAyarla(hedefeGitmeHizi);

        RaycastHit hit;
        if (Physics.Raycast(atesNoktasi.position, atesNoktasi.forward, out hit, atesEtmeMesafesi))
        {
            Debug.DrawRay(atesNoktasi.position, atesNoktasi.forward * hit.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(atesNoktasi.position, atesNoktasi.forward * atesEtmeMesafesi, Color.green);
        }

        Destroy(mermi, mermiOmru);
    }

    void OzelYetenek()
    {
        if (mermiPrefab != null && ozelYetenekPrefab != null)
        {
            eskiPrefab = mermiPrefab;
            mermiPrefab = ozelYetenekPrefab;
        }
    }

    void GeriDon()
    {
        if (eskiPrefab != null && Time.time >= ozelYetenekZamani + 0.5f) // �zel yetenek s�resi kontrol ediliyor
        {
            mermiPrefab = eskiPrefab;
            eskiPrefab = null;
            ozelYetenekAktif = false;
            ozelYetenekZamani = 0f;
            attackInProgress = false;
        }
    }


    public void AktiflestirOzelYetenek()
    {
        if (!ozelYetenekAktif)
        {
            ozelYetenekAktif = true;
            ozelYetenekZamani = Time.time; // �zel yetenek s�resini ba�lat
            OzelYetenek();
        }
    }
}