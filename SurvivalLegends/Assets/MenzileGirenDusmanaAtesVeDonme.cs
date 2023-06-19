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
    //[SerializeField] float mermiDamage = 5f;
    public GameObject mermiPrefab;
    public Transform atesNoktasi;

    private GameObject hedef;
    private bool dusmanaDonuyor = false;
    private bool menzilde = false;
    private float sonrakiAtesZamani = 0f;

    internal void AddAtesHizi(float boostAmt)
    {
        atesHizi += boostAmt;
        atesHizi = Mathf.Clamp(atesHizi, minAtesHizi, maxAtesHizi);
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
        //mermiScript.SetDamage(mermiDamage);

        RaycastHit hit;
        if (Physics.Raycast(atesNoktasi.position, atesNoktasi.forward, out hit, atesEtmeMesafesi))
        {
            // Raycast iþaretlemeyle ilgili iþlemler burada gerçekleþtirilebilir
            Debug.DrawRay(atesNoktasi.position, atesNoktasi.forward * hit.distance, Color.red);
            // Ýsabet alan hedef ile ilgili iþlemler burada gerçekleþtirilebilir
            // Örneðin, hit.collider.gameObject kullanýlarak hedefe etki edebilirsiniz
        }
        else
        {
            Debug.DrawRay(atesNoktasi.position, atesNoktasi.forward * atesEtmeMesafesi, Color.green);
        }

        Destroy(mermi, mermiOmru);

        //Debug.Log("Düþmana verilen hasar: " + mermiDamage);
    }
}