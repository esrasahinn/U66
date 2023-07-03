using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMenzileGirenDusmanaAtesVeDonme : MonoBehaviour
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

    public GameObject frozenBulletPrefab; // FrozenBullet prefab'ý

    private GameObject hedef;
    private bool attackInProgress = false;
    private float sonrakiAtesZamani = 0f;
    private bool ozelYetenekAktif = false;
    private GameObject eskiPrefab;
    private float ozelYetenekZamani = 0f;

    private Animator animator;
    private ArcherPlayerBehaviour playerBehaviour;

    internal void AddAtesHizi(float boostAmt)
    {
        atesHizi += boostAmt;
        atesHizi = Mathf.Clamp(atesHizi, minAtesHizi, maxAtesHizi);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerBehaviour = GetComponent<ArcherPlayerBehaviour>();
    }

    private void Update()
    {
        if (playerBehaviour._health <= 0)
            return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!ozelYetenekAktif)
            {
                ozelYetenekAktif = true;
                ozelYetenekZamani = Time.time; // Özel yetenek süresini baþlat
                OzelYetenek();
            }
        }

        if (hedef == null || Vector3.Distance(transform.position, hedef.transform.position) > menzilMesafesi)
        {
            HedefSec();
            attackInProgress = false;
            animator.SetBool("Idle", true);
            animator.SetBool("Attack", false);
        }

        if (hedef != null && Vector3.Distance(transform.position, hedef.transform.position) <= donmeBitisMesafesi)
        {
            if (!attackInProgress && Vector3.Distance(transform.position, hedef.transform.position) <= donmeBaslamaMesafesi)
            {
                attackInProgress = true;
                animator.SetBool("Idle", false);
                animator.SetBool("Attack", true);
            }
            DusmanaDon();
        }
        else
        {
            attackInProgress = false;
        }

        if (hedef != null && Vector3.Distance(transform.position, hedef.transform.position) <= atesEtmeMesafesi && Time.time >= sonrakiAtesZamani)
        {
            AtesEt();
            attackInProgress = true;
            sonrakiAtesZamani = Time.time + 1f / atesHizi;
            animator.SetBool("Running", attackInProgress);
        }
        else
        {
            attackInProgress = false;
        }

        if (ozelYetenekAktif)
        {
            if (Time.time >= ozelYetenekZamani + 0.5f) // Özel yetenek süresi kontrol ediliyor
            {
                GeriDon();
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            AtisYap();
        }
    }

    void AtisYap()
    {
        GameObject frozenBullet = Instantiate(frozenBulletPrefab, atesNoktasi.position, atesNoktasi.rotation);
        // Gerekirse hedefi belirle ve hýzý ayarla
        frozenBullet.GetComponent<FrozenBullet>().HedefBelirle(hedef.transform);
        frozenBullet.GetComponent<FrozenBullet>().HizAyarla(hedefeGitmeHizi);
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
        if (eskiPrefab != null && Time.time >= ozelYetenekZamani + 0.5f) // Özel yetenek süresi kontrol ediliyor
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
            ozelYetenekZamani = Time.time; // Özel yetenek süresini baþlat
            OzelYetenek();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dusman")) // Sadece düþman objeleriyle etkileþime girsin
        {
            // FrozenBullet prefab'ýný ateþ noktasýnda oluþtur
            GameObject frozenBullet = Instantiate(frozenBulletPrefab, atesNoktasi.position, atesNoktasi.rotation);
            // Hedefi belirle
            FrozenBullet frozenBulletScript = frozenBullet.GetComponent<FrozenBullet>();
            frozenBulletScript.HedefBelirle(other.gameObject.transform);
            // Hýzý ayarla
            frozenBulletScript.HizAyarla(hedefeGitmeHizi);
        }
    }
}