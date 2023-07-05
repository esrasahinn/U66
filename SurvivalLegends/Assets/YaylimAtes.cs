using UnityEngine;
using UnityEngine.UI;

public class YaylimAtes : MonoBehaviour
{
    public GameObject arrowPrefab;      // Ok prefab�n� buraya s�r�kleyip b�rak�n
    public Transform shootPoint;        // Okun ate�lendi�i noktan�n referans�

    public int arrowCount = 5;          // Yan yana ka� ok ��kacak
    public float arrowSpacing = 0.2f;   // Oklar aras�ndaki mesafe
    public int arrowDamage = 10;        // Oklar�n verdi�i hasar miktar�

    private bool isFiring = false;      // Yayl�m ate�i durumu
    private float fireRate = 2f;        // Ok atma h�z� (saniyede ka� kez)
    private float nextFireTime = 0f;    // Sonraki ate�leme zaman�

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

            // Okun h�z�n� ve y�n�n� ayarlayabilirsiniz
            arrowRigidbody.velocity = shootPoint.forward * 20f; // �rnek olarak 20 birim/s h�zda ileri y�nde atar

            spawnPosition += shootPoint.right * arrowSpacing; // Oklar� yan yana yerle�tirmek i�in sa� y�nde ilerle

            // Okun �arpma, yok olma veya di�er etkile�imlerini burada y�netebilirsiniz
            OkScript okScript = arrow.GetComponent<OkScript>();
            if (okScript != null)
            {
                okScript.DamageAmount = arrowDamage; // Okun hasar miktar�n� ayarla
            }
        }
    }
}