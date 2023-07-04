using UnityEngine;

public class YaylimAtes : MonoBehaviour
{
    public GameObject arrowPrefab;  // Ok prefabýný buraya sürükleyip býrakýn
    public Transform shootPoint;    // Okun ateþlendiði noktanýn referansý

    public float fireRate = 2f;     // Ok atma hýzý (saniyede kaç kez)
    public int arrowCount = 5;      // Yan yana kaç ok çýkacak
    public float arrowSpacing = 0.2f; // Oklar arasýndaki mesafe
    public int arrowDamage = 10;    // Oklarýn verdiði hasar miktarý

    private float nextFireTime = 0f; // Sonraki ateþleme zamaný

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            FireArrows();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void FireArrows()
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
                okScript.damageAmount = arrowDamage; // Okun hasar miktarýný ayarla
            }
        }
    }
}