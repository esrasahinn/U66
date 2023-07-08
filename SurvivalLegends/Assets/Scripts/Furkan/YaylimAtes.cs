using UnityEngine;
using UnityEngine.UI;

public class YaylimAtes : MonoBehaviour
{
    public GameObject arrowPrefab;      // Ok prefabýný buraya sürükleyip býrakýn
    public Transform shootPoint;        // Okun ateþlendiði noktanýn referansý

    public int arrowCount = 5;          // Yan yana kaç ok çýkacak
    public float arrowSpacing = 0.2f;   // Oklar arasýndaki mesafe
    public int arrowDamage = 10;        // Oklarýn verdiði hasar miktarý

    private bool isFiring = false;      // Yaylým ateþi durumu
    private float fireRate = 2f;        // Ok atma hýzý (saniyede kaç kez)
    private float nextFireTime = 0f;    // Sonraki ateþleme zamaný

    private void Update()
    {
        if (isFiring && Time.time >= nextFireTime)
        {
            FireArrows();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    public void StartFiring()
    {
        isFiring = true;
    }

    public void StopFiring()
    {
        isFiring = false;
    }

    public void FireArrows()
    {
        Vector3 spawnPosition = shootPoint.position;

        for (int i = 0; i < arrowCount; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, spawnPosition, shootPoint.rotation);
            Rigidbody arrowRigidbody = arrow.GetComponent<Rigidbody>();

            // Okun hýzýný ve yönünü ayarlayabilirsiniz
            arrowRigidbody.velocity = shootPoint.forward * 20f; // Örnek olarak 20 birim/s hýzda ileri yönde atar

            spawnPosition += shootPoint.right * arrowSpacing; // Oklarý yan yana yerleþtirmek için sað yönde ilerle

            // Okun çarpma, yok olma veya diðer etkileþimlerini burada yönetebilirsiniz
            OkScript okScript = arrow.GetComponent<OkScript>();
            if (okScript != null)
            {
                okScript.DamageAmount = arrowDamage; // Okun hasar miktarýný ayarla
            }
        }
    }
}