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
    public Transform atesNoktasi;

    private GameObject hedef;
    private bool attackInProgress = false;
    private float sonrakiAtesZamani = 0f;

    private Animator animator;
    private ArcherPlayerBehaviour playerBehaviour; // PlayerBehaviour scriptine eriþmek için referans

    internal void AddAtesHizi(float boostAmt)
    {
        atesHizi += boostAmt;
        atesHizi = Mathf.Clamp(atesHizi, minAtesHizi, maxAtesHizi);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerBehaviour = GetComponent<ArcherPlayerBehaviour>(); // PlayerBehaviour componentini al
    }

    private void Update()
    {
        if (playerBehaviour._health <= 0) // Can deðeri 0 ise ateþ etmeyi ve dönmeyi durdur
            return;

        if (hedef == null || Vector3.Distance(transform.position, hedef.transform.position) > menzilMesafesi)
        {
            HedefSec();
            attackInProgress = false;
            animator.SetBool("Idle", true); // Düþman menzilinden çýktýðýnda Idle animasyonunu etkinleþtir
            animator.SetBool("Attack", false); // Düþman menzilinden çýktýðýnda Attack animasyonunu devre dýþý býrak
        }

        if (hedef != null && Vector3.Distance(transform.position, hedef.transform.position) <= donmeBitisMesafesi)
        {
            if (!attackInProgress && Vector3.Distance(transform.position, hedef.transform.position) <= donmeBaslamaMesafesi)
            {
                attackInProgress = true;
                animator.SetBool("Idle", false); // Düþman menziline girdiðinde Idle animasyonunu devre dýþý býrak
                animator.SetBool("Attack", true); // Düþman menziline girdiðinde Attack animasyonunu etkinleþtir
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
        }
        else
        {
            attackInProgress = false;
        }

        animator.SetBool("Running", attackInProgress); // Running animasyonunu attackInProgress deðiþkenine göre ayarla
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
}